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

        internal SQLiteRecipeStore(SQLiteAsyncConnection connection, IImageStore imageStore,
            IIngredientStore ingredientStore, IPictogramStore pictogramStore) : base(connection)
        {
            _imageStore = imageStore;
            _ingredientStore = ingredientStore;
            _pictogramStore = pictogramStore;
        }

        public override IRecipeBuilder CreateBuilder() =>
            new StoreCallbackRecipeBuilder(this);

        protected internal override async Task<SQLiteRecipe> CreateObjectFromBuilder(IRecipeBuilder builder)
        {
            var info = new SQLiteRecipeInfo
            {
                Id = builder.Id.Value,
                Name = builder.Name.Value,
                Description = builder.Description.Value,
                RequiredTime = builder.RequiredTime.Value,

                ImageLoader = builder.ImageLoader.Value,
                ImageSource = builder.ImageSource.Value,

                IngredientIds = GuidCollectionToString(builder.IngredientIds.Value),
                PictogramIds = GuidCollectionToString(builder.PictogramIds.Value),
                StepIds = GuidCollectionToString(builder.StepIds.Value)
            };

            return new SQLiteRecipe(info)
            {
                Image = await _imageStore.LoadImageAsync(info.ImageLoader, info.ImageSource),

                Ingredients = await GuidEnumerableToIngredientsAsync(builder.IngredientIds.Value),
                Pictograms = await GuidEnumerableToPictogramsAsync(builder.PictogramIds.Value),

                Steps = new[] {"Placeholder 1", "Placeholder 2"}
            };
        }

        protected internal override async Task<SQLiteRecipe> CreateObjectFromInfo(SQLiteRecipeInfo info)
        {
            var ingredientIds = StringToGuidCollection(info.IngredientIds);
            var pictogramIds = StringToGuidCollection(info.PictogramIds);

            return new SQLiteRecipe(info)
            {
                Image = await _imageStore.LoadImageAsync(info.ImageLoader, info.ImageSource),

                Ingredients = await GuidEnumerableToIngredientsAsync(ingredientIds),
                Pictograms = await GuidEnumerableToPictogramsAsync(pictogramIds),

                Steps = new[] {"Placeholder 1", "Placeholder 2"}
            };
        }

        private static string GuidCollectionToString(IReadOnlyCollection<Guid> ids)
        {
            if (ids is null || ids.Count == 0)
                return string.Empty;

            if (ids.Count == 1)
                return ids.First().ToString();

            return ids
                .Select(id => id.ToString())
                .Aggregate((id, id2) => $"{id};{id2}");
        }

        private static IReadOnlyCollection<Guid> StringToGuidCollection(string str)
        {
            if (str is null || str == string.Empty)
                return new Guid[0];

            return str
                .Split(';')
                .Select(Guid.Parse)
                .ToArray();
        }

        private async Task<IReadOnlyList<IIngredient>> GuidEnumerableToIngredientsAsync(IEnumerable<Guid> ids)
        {
            if (ids is null)
                return new IIngredient[0];

            var tasks = ids.Select(id => _ingredientStore.LoadAsync(id));
            var ingredients = new List<IIngredient>();

            foreach (var task in tasks)
                ingredients.Add(await task);

            return ingredients;
        }

        private async Task<IReadOnlyList<IPictogram>> GuidEnumerableToPictogramsAsync(IEnumerable<Guid> ids)
        {
            if (ids is null)
                return new IPictogram[0];

            var tasks = ids.Select(id => _pictogramStore.LoadAsync(id));
            var pictograms = new List<IPictogram>();

            foreach (var task in tasks)
                pictograms.Add(await task);

            return pictograms;
        }
    }
}