using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Models.Impl.SQLite.Steps;
using CooKit.Models.Impl.Steps;
using CooKit.Models.Steps;
using SQLite;

namespace CooKit.Services.Impl.SQLite
{
    internal sealed class SQLiteRecipeStepStore : SQLiteStoreBase<IRecipeStep, IRecipeStepBuilder, SQLiteRecipeStep, SQLiteRecipeStepInfo>, IRecipeStepStore
    {
        private Dictionary<Guid, SQLiteTextRecipeStepInfo> _idToTextInfo;
        private Dictionary<Guid, SQLiteBigImageRecipeStepInfo> _idToBigImageInfo;

        private readonly IImageStore _imageStore;

        internal SQLiteRecipeStepStore(SQLiteAsyncConnection connection, IImageStore imageStore)
            : base(connection) => _imageStore = imageStore;

        public override IRecipeStepBuilder CreateBuilder() =>
            new StoreCallbackRecipeStepBuilder(this);

        private protected override async Task PreInitAsync()
        {
            await Connection.CreateTableAsync<SQLiteTextRecipeStepInfo>();
            await Connection.CreateTableAsync<SQLiteBigImageRecipeStepInfo>();

            var textInfoTask = Connection
                .Table<SQLiteTextRecipeStepInfo>()
                .ToArrayAsync();

            var bigImageInfoTask = Connection
                .Table<SQLiteBigImageRecipeStepInfo>()
                .ToArrayAsync();

            await Task.WhenAll(textInfoTask, bigImageInfoTask);

            _idToTextInfo = textInfoTask.Result.ToDictionary(info => info.Id);
            _idToBigImageInfo = bigImageInfoTask.Result.ToDictionary(info => info.Id);
        }

        private protected override Task RemoveObjectFromDatabase(SQLiteRecipeStep storable)
        {
            var baseTask = base.RemoveObjectFromDatabase(storable);
            var task = Connection.DeleteAsync(storable.SpecificInternalInfo);

            return Task.WhenAll(baseTask, task);
        }

        private protected override async Task<SQLiteRecipeStepInfo> CreateInfoFromBuilder(IRecipeStepBuilder builder)
        {
            RecipeStepType type;

            switch (builder)
            {
                case ITextRecipeStepBuilder specified:
                    await BuilderToTextInfo(specified);
                    type = RecipeStepType.TextOnly;
                    break;

                case IBigImageRecipeStepBuilder specified:
                    await BuilderToBigImageInfo(specified);
                    type = RecipeStepType.BigImage;
                    break;

                default:
                    return null;
            }

            return new SQLiteRecipeStepInfo
            {
                Id = builder.Id.Value,
                Type = type
            };
        }

        private protected override async Task<SQLiteRecipeStep> CreateObjectFromInfo(SQLiteRecipeStepInfo info)
        {
            var step = info.Type switch
            {
                RecipeStepType.TextOnly => GenericInfoToTextStep(info),
                RecipeStepType.BigImage => await GenericInfoToBigImageStep(info),
                _ => null
            };

            return step;
        }

        #region Generic Info to Implementation

        private SQLiteRecipeStep GenericInfoToTextStep(SQLiteRecipeStepInfo info) =>
            _idToTextInfo.ContainsKey(info.Id) ? new SQLiteTextRecipeStep(_idToTextInfo[info.Id], info) : null;

        private Task<SQLiteRecipeStep> GenericInfoToBigImageStep(SQLiteRecipeStepInfo info)
        {
            if (!_idToBigImageInfo.TryGetValue(info.Id, out var specificInfo))
                return Task.FromResult(default(SQLiteRecipeStep));

            var step = new SQLiteBigImageRecipeStep(specificInfo, info);

            return _imageStore
                .LoadImageAsync(specificInfo.ImageLoader, specificInfo.ImageSource)
                .ContinueWith(imageTask =>
                {
                    step.Image = imageTask.Result;
                    return (SQLiteRecipeStep) step;
                });
        }
        
        #endregion

        #region Builder to Specific Info

        private Task BuilderToTextInfo(ITextRecipeStepBuilder builder)
        {
            var info = new SQLiteTextRecipeStepInfo
            {
                Id = builder.Id.Value,
                Text = builder.Text.Value
            };

            return Connection
                .InsertAsync(info)
                .ContinueWith(_ => _idToTextInfo[info.Id] = info);
        }

        private Task BuilderToBigImageInfo(IBigImageRecipeStepBuilder builder)
        {
            var info = new SQLiteBigImageRecipeStepInfo
            {
                Id = builder.Id.Value,
                ImageLoader = builder.ImageLoader.Value,
                ImageSource = builder.ImageSource.Value
            };

            return Connection
                .InsertAsync(info)
                .ContinueWith(_ => _idToBigImageInfo[info.Id] = info);
        }

        #endregion
    }
}
