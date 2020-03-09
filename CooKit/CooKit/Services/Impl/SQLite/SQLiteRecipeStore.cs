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

        private Queue<IRecipe> _recipeQueue;

        internal SQLiteRecipeStore(string sqlPath, IImageStore imageStore)
        {
            _connection = new SQLiteAsyncConnection(sqlPath);
            _imageStore = imageStore;

            _recipeQueue = null;
        }

        public IRecipe GetNextRecipe()
        {
            if (_recipeQueue.Count == 0)
                return null;

            return _recipeQueue.Dequeue();
        }

        public async Task<IRecipe> GetNextRecipeAsync() =>
            await Task.Run(GetNextRecipe);

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

                recipeList.Add(new MockRecipe(
                    info.Name,
                    info.Description,

                    _imageStore.LoadImage(info.ImageLoader, info.ImageSource),

                    recipePictograms, recipeIngredients, new[] { "Step 1", "Step 2" }));
            }

            LoadedRecipes = recipeList;
            _recipeQueue = new Queue<IRecipe>(recipeList);
        }

        private async Task<IEnumerable<T>> LoadInfoAsync<T>() where T : new() =>
            await _connection
                .Table<T>()
                .ToArrayAsync();

        private SQLiteIngredient InfoToIngredient(SQLiteIngredientInfo info) =>
            new SQLiteIngredient
            {
                Id = Guid.Parse(info.Id),
                Name = info.Name,
                Icon = _imageStore.LoadImage(info.ImageLoader, info.ImageSource)
            };

        private SQLitePictogram InfoToPictogram(SQLitePictogramInfo info) =>
            new SQLitePictogram
            {
                Id = Guid.Parse(info.Id),
                Name = info.Name,
                Description = info.Description,
                Image = _imageStore.LoadImage(info.ImageLoader, info.ImageSource)
            };

        public void Dispose() =>
            _connection.CloseAsync().Wait();
    }
}