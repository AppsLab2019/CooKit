using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CooKit.Models;
using CooKit.Models.Impl;
using CooKit.Models.Impl.Generic;
using CooKit.Models.Impl.SQLite;
using SQLite;
using Xamarin.Forms;

namespace CooKit.Services.Impl.SQLite
{
    internal sealed class SQLiteRecipeStore : SQLiteStoreBase<IRecipe, IRecipeBuilder, SQLiteRecipeInternalInfo>, IRecipeStore
    {
        private readonly IIngredientStore _ingredientStore;
        private readonly IPictogramStore _pictogramStore;
        private readonly IStepStore _stepStore;

        private readonly ImageSource _defaultImage;

        internal SQLiteRecipeStore(SQLiteAsyncConnection connection, IImageStore imageStore, IIngredientStore ingredientStore,
            IPictogramStore pictogramStore, IStepStore stepStore) : base(connection, imageStore)
        {
            _ingredientStore = ingredientStore;
            _pictogramStore = pictogramStore;
            _stepStore = stepStore;

            // TODO: replace with default image
            _defaultImage = null;
        }

        public override IRecipeBuilder CreateBuilder() =>
            new StoreCallbackRecipeBuilder(this);

        private protected override async Task<IRecipe> InternalInfoToObject(SQLiteRecipeInternalInfo info)
        {
            if (info is null)
                throw new ArgumentNullException(nameof(info));

            var recipe = new GenericRecipe
            {
                Id = info.Id,
                Name = info.Name,
                Description = info.Description,
                RequiredTime = info.RequiredTime
            };

            var imageTask = SafeImageLoadAsync(info.ImageLoader, info.ImageSource, _defaultImage);
            var ingredientsTask = ParseAndLoad(_ingredientStore, info.Ingredients);
            var pictogramsTask = ParseAndLoad(_pictogramStore, info.Pictograms);
            var stepsTask = ParseAndLoad(_stepStore, info.Steps);

            await Task.WhenAll(imageTask, ingredientsTask, pictogramsTask, stepsTask);

            recipe.Image = imageTask.Result;
            recipe.Ingredients = ingredientsTask.Result;
            recipe.Pictograms = pictogramsTask.Result;
            recipe.Steps = stepsTask.Result;

            return recipe;
        }

        private protected override Task<SQLiteRecipeInternalInfo> BuilderToInternalInfo(IRecipeBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            var info = new SQLiteRecipeInternalInfo
            {
                Id = builder.Id.Value,
                Name = builder.Name.Value,
                Description = builder.Description.Value,

                ImageLoader = builder.ImageLoader.Value,
                ImageSource = builder.ImageSource.Value,
                RequiredTime = builder.RequiredTime.Value,

                Ingredients = IdsToString(builder.IngredientIds.Value),
                Pictograms = IdsToString(builder.PictogramIds.Value),
                Steps = IdsToString(builder.StepIds.Value)
            };

            return Task.FromResult(info);
        }

        #region Helper Functions

        private static Task<IReadOnlyList<T>> ParseAndLoad<T, TBuilder>(IStoreBase<T, TBuilder> store, string strIds)
            where T : IStorable
        {
            if (strIds is null || strIds == string.Empty)
                return Task.FromResult(new T[0] as IReadOnlyList<T>);

            var ids = StringToIds(strIds);
            return StoreToStorables(store, ids);
        }

        private static async Task<IReadOnlyList<T>> StoreToStorables<T, TBuilder>(IStoreBase<T, TBuilder> store, IEnumerable<Guid> ids)
            where T : IStorable
        {
            if (ids is null)
                return null;

            var tasks = ids.Select(store.LoadAsync).ToArray();
            
            await Task.WhenAll(tasks);
            return tasks.Select(task => task.Result).ToArray();
        }

        private static string IdsToString(IEnumerable<Guid> ids)
        {
            if (ids is null)
                return null;

            var builder = new StringBuilder();

            foreach (var id in ids)
            {
                builder.Append(id.ToString());
                builder.Append(';');
            }

            if (builder.Length == 0)
                return string.Empty;

            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        private static IEnumerable<Guid> StringToIds(string str)
        {
            if (str is null || str == string.Empty)
                return null;

            return str.Split(';').Select(Guid.Parse);
        }

        #endregion
    }
}
