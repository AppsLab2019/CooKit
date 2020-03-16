using System;
using System.Collections.Generic;
using CooKit.Models;
using CooKit.Models.Impl;
using CooKit.Models.Impl.SQLite;
using SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace CooKit.Services.Impl.SQLite
{
    internal sealed class SQLiteRecipeStore : SQLiteStoreBase<IRecipe, IRecipeBuilder, SQLiteRecipe, SQLiteRecipeInfo>,
        IRecipeStore
    {
        private readonly IImageStore _imageStore;
        private readonly IIngredientStore _ingredientStore;
        private readonly IPictogramStore _pictogramStore;
        private readonly IRecipeStepStore _recipeStepStore;

        internal SQLiteRecipeStore(SQLiteAsyncConnection connection, IImageStore imageStore, IIngredientStore ingredientStore, 
            IPictogramStore pictogramStore, IRecipeStepStore recipeStepStore) : base(connection)
        {
            _imageStore = imageStore;
            _ingredientStore = ingredientStore;
            _pictogramStore = pictogramStore;
            _recipeStepStore = recipeStepStore;
        }

        public override IRecipeBuilder CreateBuilder() =>
            new StoreCallbackRecipeBuilder(this);

        private protected override Task<SQLiteRecipeInfo> CreateInfoFromBuilder(IRecipeBuilder builder) =>
            Task.Run(() => new SQLiteRecipeInfo
            {
                Id = builder.Id.Value,
                Name = builder.Name.Value,
                Description = builder.Description.Value,
                RequiredTime = builder.RequiredTime.Value,

                ImageLoader = builder.ImageLoader.Value,
                ImageSource = builder.ImageSource.Value,

                IngredientIds = GuidsToString(builder.IngredientIds.Value),
                PictogramIds = GuidsToString(builder.PictogramIds.Value),
                StepIds = GuidsToString(builder.StepIds.Value)
            });

        private protected override async Task<SQLiteRecipe> CreateObjectFromInfo(SQLiteRecipeInfo info)
        {
            var ingredientIds = StringToGuids(info.IngredientIds);
            var pictogramIds = StringToGuids(info.PictogramIds);
            var recipeStepIds = StringToGuids(info.StepIds);

            return new SQLiteRecipe(info)
            {
                Image = await _imageStore.LoadImageAsync(info.ImageLoader, info.ImageSource),

                Ingredients = await GuidsToStorableAsync(_ingredientStore, ingredientIds),
                Pictograms = await GuidsToStorableAsync(_pictogramStore, pictogramIds),
                Steps = await GuidsToStorableAsync(_recipeStepStore, recipeStepIds)
            };
        }

        private static string GuidsToString(IReadOnlyCollection<Guid> ids)
        {
            if (ids is null || ids.Count == 0)
                return string.Empty;

            if (ids.Count == 1)
                return ids.First().ToString();

            return ids
                .Select(id => id.ToString())
                .Aggregate((id, id2) => $"{id};{id2}");
        }

        private static IEnumerable<Guid> StringToGuids(string str)
        {
            if (str is null || str == string.Empty)
                return new Guid[0];

            return str
                .Split(';')
                .Select(Guid.Parse)
                .ToArray();
        }

        private static async Task<IReadOnlyList<T>> GuidsToStorableAsync<T, TBuilder>(
            IStoreBase<T, TBuilder> store, IEnumerable<Guid> ids) where T : IStorable
        {
            if (ids is null)
                return new T[0];

            var tasks = ids
                .Select(store.LoadAsync)
                .ToArray();

            await Task.WhenAll(tasks);

            return tasks
                .Select(task => task.Result)
                .ToArray();
        }
    }
}