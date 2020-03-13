using CooKit.Models;
using CooKit.Models.Impl;
using CooKit.Models.Impl.SQLite;
using SQLite;
using System.Linq;

namespace CooKit.Services.Impl.SQLite
{
    internal sealed class SQLiteRecipeStore : SQLiteStoreBase<IRecipe, IRecipeBuilder, SQLiteRecipe, SQLiteRecipeInfo>, IRecipeStore
    {
        private readonly IImageStore _imageStore;

        internal SQLiteRecipeStore(SQLiteAsyncConnection connection, IImageStore imageStore) 
            : base(connection)
        {
            _imageStore = imageStore;
        }

        public override IRecipeBuilder CreateBuilder() =>
            new StoreCallbackRecipeBuilder(this);

        protected internal override SQLiteRecipe CreateObjectFromBuilder(IRecipeBuilder builder)
        {
            var info = new SQLiteRecipeInfo
            {
                Id = builder.Id.Value,
                Name = builder.Name.Value,
                Description = builder.Description.Value,

                ImageLoader = builder.ImageLoader.Value,
                ImageSource = builder.ImageSource.Value,

                IngredientIds = builder.IngredientIds.Value?
                    .Select(id => id.ToString())
                    .Aggregate((id, id2) => $"{id};{id2}"),

                PictogramIds = builder.PictogramIds.Value?
                    .Select(id => id.ToString())
                    .Aggregate((id, id2) => $"{id};{id2}"),

                StepIds = builder.StepIds.Value
                    .Select(id => id.ToString())
                    .Aggregate((id, id2) => $"{id};{id2}")
            };

            return new SQLiteRecipe(info)
            {
                MainImage = _imageStore.LoadImage(info.ImageLoader, info.ImageSource),

                Ingredients = new IIngredient[0],
                Pictograms = new IPictogram[0],

                Steps = new[] { "Placeholder 1", "Placeholder 2" }
            };
        }

        protected internal override SQLiteRecipe CreateObjectFromInfo(SQLiteRecipeInfo info)
        {
            //var recipeIngredients = info.IngredientIds
            //    .Split(';')
            //    .Select(Guid.Parse)
            //    .Select(id => _ingredients[id])
            //    .ToArray();

            //var recipePictograms = info.PictogramIds
            //    .Split(';')
            //    .Select(Guid.Parse)
            //    .Select(id => _pictograms[id])
            //    .ToArray();

            return new SQLiteRecipe(info)
            {
                MainImage = _imageStore.LoadImage(info.ImageLoader, info.ImageSource),

                Ingredients = new IIngredient[0],
                Pictograms = new IPictogram[0],
                Steps = new[] { "Placeholder 1", "Placeholder 2" }
            };
        }
    }
}