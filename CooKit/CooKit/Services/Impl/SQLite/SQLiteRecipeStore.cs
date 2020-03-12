using CooKit.Models;
using CooKit.Models.Impl;
using CooKit.Models.Impl.SQLite;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CooKit.Services.Impl.SQLite
{
    // TODO: move ingredient store to its own class
    // TODO: move pictogram store to its own class
    public sealed class SQLiteRecipeStore : IRecipeStore, IDisposable, IIngredientStore, IPictogramStore
    {
        public IReadOnlyList<IRecipe> LoadedRecipes => _loadedRecipes;
        private readonly List<IRecipe> _loadedRecipes;

        public IReadOnlyList<IIngredient> LoadedIngredients { get; private set; }
        public IReadOnlyList<IPictogram> LoadedPictograms { get; private set; }

        private readonly SQLiteAsyncConnection _connection;
        private readonly IImageStore _imageStore;

        private Dictionary<Guid, IngredientImpl> _ingredients;
        private Dictionary<Guid, PictogramImpl> _pictograms;
        private readonly Dictionary<Guid, SQLiteRecipe> _recipes;

        public event PropertyChangedEventHandler PropertyChanged;

        internal SQLiteRecipeStore(string sqlPath, IImageStore imageStore)
        {
            _connection = new SQLiteAsyncConnection(sqlPath);
            _imageStore = imageStore;

            _loadedRecipes = new List<IRecipe>();
            _recipes = new Dictionary<Guid, SQLiteRecipe>();
        }

        public IRecipe LoadRecipe() => null;

        public IRecipeBuilder CreateRecipeBuilder() =>
            new SQLiteRecipeBuilder(this);

        public Task<IRecipe> LoadRecipeAsync() =>
            Task.FromResult(default(IRecipe));

        public IRecipe LoadRecipe(Guid id) => 
            _recipes.ContainsKey(id) ? _recipes[id] : null;

        public async Task<IRecipe> LoadRecipeAsync(Guid id) =>
            await Task.Run(() => LoadRecipe(id));

        public void AddRecipe(IRecipeBuilder recipeBuilder) =>
            AddRecipeAsync(recipeBuilder).Wait();

        public async Task AddRecipeAsync(IRecipeBuilder recipeBuilder)
        {
            var info = new SQLiteRecipeInfo
            {
                Id = recipeBuilder.Id.Value,
                Name = recipeBuilder.Name.Value,
                Description = recipeBuilder.Description.Value,

                ImageLoader = recipeBuilder.ImageLoader.Value,
                ImageSource = recipeBuilder.ImageSource.Value,

                IngredientIds = recipeBuilder.IngredientIds.Value?
                    .Select(id => id.ToString())
                    .Aggregate((id, id2) => $"{id};{id2}"),

                PictogramIds = recipeBuilder.PictogramIds.Value?
                    .Select(id => id.ToString())
                    .Aggregate((id, id2) => $"{id};{id2}"),

                StepIds = recipeBuilder.StepIds.Value
                    .Select(id => id.ToString())
                    .Aggregate((id, id2) => $"{id};{id2}")
            };

            await _connection.InsertAsync(info);

            var recipe = new SQLiteRecipe(info)
            {
                MainImage = await _imageStore.LoadImageAsync(info.ImageLoader, info.ImageSource),

                Ingredients = recipeBuilder.IngredientIds.Value?
                    .Select(id => _ingredients[id])
                    .ToArray() ?? new IIngredient[0],

                Pictograms = recipeBuilder.IngredientIds.Value?
                    .Select(id => _pictograms[id])
                    .ToArray() ?? new IPictogram[0],

                Steps = new[] {"Placeholder 1", "Placeholder 2"}
            };

            _loadedRecipes.Add(recipe);
            _recipes.Add(recipe.Id, recipe);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoadedRecipes)));
        }

        public bool RemoveRecipe(Guid id) =>
            RemoveRecipeAsync(id).Result;

        public async Task<bool> RemoveRecipeAsync(Guid id)
        {
            if (!_recipes.ContainsKey(id))
                return false;

            var recipe = _recipes[id];

            await _connection.DeleteAsync(recipe.InternalInfo);
            _recipes.Remove(id);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoadedRecipes)));

            return true;
        }

        public async Task LoadRecipesAsync()
        {
            await _connection.CreateTableAsync<SQLiteRecipeInfo>();
            await _connection.CreateTableAsync<SQLiteIngredientInfo>();
            await _connection.CreateTableAsync<SQLitePictogramInfo>();

            var recipeInfos = await LoadInfoAsync<SQLiteRecipeInfo>();
            var ingredientInfos = await LoadInfoAsync<SQLiteIngredientInfo>();
            var pictogramInfos = await LoadInfoAsync<SQLitePictogramInfo>();

            _ingredients = ingredientInfos
                .Select(InfoToIngredient)
                .ToDictionary(ingredient => ingredient.Id);

            _pictograms = pictogramInfos
                .Select(InfoToPictogram)
                .ToDictionary(pictogram => pictogram.Id);

            LoadedIngredients = new List<IIngredient>(_ingredients.Values);
            LoadedPictograms = new List<IPictogram>(_pictograms.Values);

            foreach (var info in recipeInfos)
            {
                var recipeIngredients = info.IngredientIds
                    .Split(';')
                    .Select(Guid.Parse)
                    .Select(id => _ingredients[id])
                    .ToArray();

                var recipePictograms = info.PictogramIds
                    .Split(';')
                    .Select(Guid.Parse)
                    .Select(id => _pictograms[id])
                    .ToArray();

                var recipe = new SQLiteRecipe(info)
                {
                    MainImage = _imageStore.LoadImage(info.ImageLoader, info.ImageSource),

                    Ingredients = recipeIngredients,
                    Pictograms = recipePictograms,
                    Steps = new[] { "Placeholder 1", "Placeholder 2" }
                };

                _loadedRecipes.Add(recipe);
                _recipes.Add(info.Id, recipe);
            }
        }

        private async Task<IEnumerable<T>> LoadInfoAsync<T>() where T : new() =>
            await _connection
                .Table<T>()
                .ToArrayAsync();

        private IngredientImpl InfoToIngredient(SQLiteIngredientInfo info) =>
            new IngredientImpl(info.Id, info.Name,
                _imageStore.LoadImage(info.ImageLoader, info.ImageSource));

        private PictogramImpl InfoToPictogram(SQLitePictogramInfo info) =>
            new PictogramImpl(info.Id, info.Name, info.Description,
                _imageStore.LoadImage(info.ImageLoader, info.ImageSource));

        public void Dispose() =>
            _connection.CloseAsync().Wait();
    }
}