using CooKit.Models;
using CooKit.Models.Impl.SQLite;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CooKit.Services.Impl
{
    public sealed class SQLiteRecipeStore : IRecipeStore, IDisposable
    {
        public IReadOnlyList<IRecipe> LoadedRecipes { get; }

        private readonly SQLiteAsyncConnection _connection;
        private readonly IImageStore _imageStore;

        private readonly int _recipeCount;

        public SQLiteRecipeStore(string sqlPath, IImageStore imageStore)
        {
            _connection = new SQLiteAsyncConnection(sqlPath);
            _imageStore = imageStore;

            _recipeCount = _connection
                .Table<SQLiteRecipeInfo>()
                .CountAsync()
                .Result;
        }

        public IRecipe LoadRecipe()
        {
            throw new NotImplementedException();
        }

        public async Task<IRecipe> LoadRecipeAsync()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IRecipe> LoadRecipes(int count)
        {
            // TODO: check if count is less or equal then number of available recipes
            var recipes = new IRecipe[count];

            for (var i = 0; i < count; i++)
                recipes[i] = LoadRecipe();

            return recipes;
        }

        public async Task<IReadOnlyList<IRecipe>> LoadRecipesAsync(int count)
        {
            // TODO: check if count is less or equal then number of available recipes
            var recipes = new IRecipe[count];

            for (var i = 0; i < count; i++)
                recipes[i] = await LoadRecipeAsync();

            return recipes;
        }

        public void Dispose() =>
            _connection.CloseAsync().Wait();
    }
}