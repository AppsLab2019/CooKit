using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Models;
using CooKit.Models.Impl.Generic;
using CooKit.Models.Impl.SQLite;
using CooKit.Models.Impl.Steps;
using CooKit.Models.Steps;
using SQLite;
using Xamarin.Forms;

namespace CooKit.Services.Impl.SQLite
{
    internal sealed class SQLiteStepStore : SQLiteStoreBase<IRecipeStep, IRecipeStepBuilder, SQLiteStepInternalInfo>, IRecipeStepStore
    {
        private IDictionary<Guid, SQLiteTextStepInternalInfo> _unhandledTextInfos;
        private IDictionary<Guid, SQLiteImageStepInternalInfo> _unhandledImageInfos;

        private readonly ImageSource _defaultImage;

        internal SQLiteStepStore(SQLiteAsyncConnection connection, IImageStore imageStore) : base(connection, imageStore)
        {
            // TODO: replace with default image
            _defaultImage = null;
        }

        public override IRecipeStepBuilder CreateBuilder() =>
            new StoreCallbackRecipeStepBuilder(this);

        private protected override async Task PreInitAsync()
        {
            await base.PreInitAsync();
            await Connection.CreateTablesAsync<SQLiteTextStepInternalInfo, SQLiteImageStepInternalInfo>();

            _unhandledTextInfos = await ExportTable<SQLiteTextStepInternalInfo>();
            _unhandledImageInfos = await ExportTable<SQLiteImageStepInternalInfo>();
        }

        private async Task<IDictionary<Guid, T>> ExportTable<T>() where T : IStorable, new()
        {
            var infos = await Connection
                .Table<T>()
                .ToArrayAsync();

            return infos.ToDictionary(info => info.Id);
        }

        private protected override async Task<IRecipeStep> InternalInfoToObject(SQLiteStepInternalInfo info)
        {
            if (info is null)
                throw new ArgumentNullException(nameof(info));

            var id = info.Id;
            var step = info.Type switch
            {
                RecipeStepType.TextOnly => MapTextStep(id),
                RecipeStepType.BigImage => await MapImageStep(id),
                _ => throw new Exception("Unknown step type!")
            };

            return step;
        }

        private protected override Task<SQLiteStepInternalInfo> BuilderToInternalInfo(IRecipeStepBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder switch
            {
                ITextRecipeStepBuilder specific => RegisterInfoFromBuilder(specific),
                IBigImageRecipeStepBuilder specific => RegisterInfoFromBuilder(specific),
                _ => throw new Exception("Unknown builder type!")
            };
        }

        #region Mapping functions

        private IRecipeStep MapTextStep(Guid id)
        {
            if (!_unhandledTextInfos.Remove(id, out var info))
                throw new ArgumentException(nameof(id));

            return new GenericTextStep
            {
                Id = info.Id,
                Type = RecipeStepType.TextOnly,
                Text = info.Text
            };
        }

        private Task<IRecipeStep> MapImageStep(Guid id)
        {
            if (!_unhandledImageInfos.Remove(id, out var info))
                throw new ArgumentException(nameof(id));

            var step = new GenericImageStep
            {
                Id = info.Id,
                Type = RecipeStepType.BigImage
            };

            return SafeImageLoadAsync(info.ImageLoader, info.ImageSource, _defaultImage).ContinueWith(imageTask =>
                {
                    step.Image = imageTask.Result;
                    return step as IRecipeStep;
                });
        }

        #endregion

        #region Building helper functions

        private Task<SQLiteStepInternalInfo> RegisterInfoFromBuilder(ITextRecipeStepBuilder builder)
        {
            var info = new SQLiteTextStepInternalInfo
            {
                Id = builder.Id.Value,
                Text = builder.Text.Value
            };

            _unhandledTextInfos.Add(info.Id, info);
            return InsertInfoAndCreateGeneric(info, RecipeStepType.TextOnly);
        }

        private Task<SQLiteStepInternalInfo> RegisterInfoFromBuilder(IBigImageRecipeStepBuilder builder)
        {
            var info = new SQLiteImageStepInternalInfo
            {
                Id = builder.Id.Value,
                ImageLoader = builder.ImageLoader.Value,
                ImageSource = builder.ImageSource.Value
            };

            _unhandledImageInfos.Add(info.Id, info);
            return InsertInfoAndCreateGeneric(info, RecipeStepType.BigImage);
        }

        private Task<SQLiteStepInternalInfo> InsertInfoAndCreateGeneric<T>(T info, RecipeStepType type)
            where T : IStorable => Connection.InsertAsync(info).ContinueWith(_ => CreateGenericInfo(info.Id, type));

        private static SQLiteStepInternalInfo CreateGenericInfo(Guid id, RecipeStepType type) =>
            new SQLiteStepInternalInfo
            {
                Id = id,
                Type = type
            };

        #endregion
    }
}
