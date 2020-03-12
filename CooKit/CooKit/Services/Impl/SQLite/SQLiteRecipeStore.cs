using CooKit.Models;
using CooKit.Models.Impl;
using CooKit.Models.Impl.SQLite;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CooKit.Services.Impl.SQLite
{
    public sealed class SQLiteRecipeStore : IRecipeStore, IDisposable
    {
        public IReadOnlyList<IRecipe> LoadedRecipes { get; private set; }

        private readonly SQLiteAsyncConnection _connection;
        private readonly IImageStore _imageStore;

        private readonly Dictionary<Guid, IRecipe> _recipes;

        internal SQLiteRecipeStore(string sqlPath, IImageStore imageStore)
        {
            _connection = new SQLiteAsyncConnection(sqlPath);
            _imageStore = imageStore;

            _recipes = new Dictionary<Guid, IRecipe>();
        }

        public IRecipe LoadRecipe() => null;

        public Task<IRecipe> LoadRecipeAsync() =>
            Task.FromResult(default(IRecipe));

        public IRecipe LoadRecipe(Guid id) => 
            _recipes.ContainsKey(id) ? _recipes[id] : null;

        public async Task<IRecipe> LoadRecipeAsync(Guid id) =>
            await Task.Run(() => LoadRecipe(id));

        public async Task LoadRecipesAsync()
        {
            await _connection.CreateTableAsync<SQLiteRecipeInfo>();
            await _connection.CreateTableAsync<SQLiteIngredientInfo>();
            await _connection.CreateTableAsync<SQLitePictogramInfo>();

            var recipeInfos = await LoadInfoAsync<SQLiteRecipeInfo>();
            var ingredientInfos = await LoadInfoAsync<SQLiteIngredientInfo>();
            var pictogramInfos = await LoadInfoAsync<SQLitePictogramInfo>();

            var ingredients = ingredientInfos
                .Select(InfoToIngredient)
                .ToDictionary(ingredient => ingredient.Id);

            var pictograms = pictogramInfos
                .Select(InfoToPictogram)
                .ToDictionary(pictogram => pictogram.Id);

            var recipeList = new List<IRecipe>();

            foreach (var info in recipeInfos)
            {
                var recipeIngredients = info.IngredientIds
                    .Split(';')
                    .Select(Guid.Parse)
                    .Select(id => ingredients[id])
                    .ToArray();

                var recipePictograms = info.PictogramIds
                    .Split(';')
                    .Select(Guid.Parse)
                    .Select(id => pictograms[id])
                    .ToArray();

                var recipe = new RecipeImpl(Guid.Parse(info.Id))
                {
                    Name = info.Name,
                    Description = info.Description,
                    MainImage = _imageStore.LoadImage(info.ImageLoader, info.ImageSource),

                    Ingredients = recipeIngredients,
                    Pictograms = recipePictograms,
                    Steps = new[] { "Placeholder 1", "Placeholder 2" }
                };

                recipeList.Add(recipe);
                _recipes.Add(Guid.Parse(info.Id), recipe);
            }

            LoadedRecipes = recipeList;
        }

        private async Task<IEnumerable<T>> LoadInfoAsync<T>() where T : new() =>
            await _connection
                .Table<T>()
                .ToArrayAsync();

        private IIngredient InfoToIngredient(SQLiteIngredientInfo info) =>
            new IngredientImpl(Guid.Parse(info.Id), info.Name,
                _imageStore.LoadImage(info.ImageLoader, info.ImageSource));

        private IPictogram InfoToPictogram(SQLitePictogramInfo info) =>
            new PictogramImpl(Guid.Parse(info.Id), info.Name, info.Description,
                _imageStore.LoadImage(info.ImageLoader, info.ImageSource));

        public void Dispose() =>
            _connection.CloseAsync().Wait();
    }
}