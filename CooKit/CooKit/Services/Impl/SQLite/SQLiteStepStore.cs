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
    internal sealed class SQLiteStepStore : SQLiteStoreBase<IStep, IStepBuilder, SQLiteStepInternalInfo>, IStepStore
    {
        private IDictionary<Guid, SQLiteTextStepInternalInfo> _unhandledTextInfos;
        private IDictionary<Guid, SQLiteImageStepInternalInfo> _unhandledImageInfos;

        private readonly ImageSource _defaultImage;

        internal SQLiteStepStore(SQLiteAsyncConnection connection, IImageStore imageStore) : base(connection, imageStore)
        {
            // TODO: replace with default image
            _defaultImage = null;
        }

        public override IStepBuilder CreateBuilder() =>
            new StoreCallbackStepBuilder(this);

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

        private protected override Task RemoveInternalAsync(IStep obj) =>
            obj.Type switch
            {
                StepType.Text => Connection.DeleteAsync<SQLiteTextStepInternalInfo>(obj.Id),
                StepType.Image => Connection.DeleteAsync<SQLiteImageStepInternalInfo>(obj.Id),
                _ => throw new NotSupportedException("Unknown step type!")
            };

        private protected override async Task<IStep> InternalInfoToObject(SQLiteStepInternalInfo info)
        {
            if (info is null)
                throw new ArgumentNullException(nameof(info));

            var id = info.Id;
            var step = info.Type switch
            {
                StepType.Text => MapTextStep(id),
                StepType.Image => await MapImageStep(id),
                _ => throw new NotSupportedException("Unknown step type!")
            };

            return step;
        }

        private protected override Task<SQLiteStepInternalInfo> BuilderToInternalInfo(IStepBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder switch
            {
                ITextStepBuilder specific => RegisterInfoFromBuilder(specific),
                IImageStepBuilder specific => RegisterInfoFromBuilder(specific),
                _ => throw new NotSupportedException("Unknown builder type!")
            };
        }

        #region Mapping functions

        private IStep MapTextStep(Guid id)
        {
            if (!_unhandledTextInfos.Remove(id, out var info))
                throw new ArgumentException(nameof(id));

            return new GenericTextStep
            {
                Id = info.Id,
                Type = StepType.Text,
                Text = info.Text
            };
        }

        private async Task<IStep> MapImageStep(Guid id)
        {
            if (!_unhandledImageInfos.Remove(id, out var info))
                throw new ArgumentException(nameof(id));

            return new GenericImageStep
            {
                Id = info.Id,
                Type = StepType.Image,
                Image = await SafeImageLoadAsync(info.ImageLoader, info.ImageSource, _defaultImage)
            };
        }

        #endregion

        #region Building helper functions

        private Task<SQLiteStepInternalInfo> RegisterInfoFromBuilder(ITextStepBuilder builder)
        {
            var info = new SQLiteTextStepInternalInfo
            {
                Id = builder.Id.Value,
                Text = builder.Text.Value
            };

            _unhandledTextInfos.Add(info.Id, info);
            return InsertInfoAndCreateGeneric(info, StepType.Text);
        }

        private Task<SQLiteStepInternalInfo> RegisterInfoFromBuilder(IImageStepBuilder builder)
        {
            var info = new SQLiteImageStepInternalInfo
            {
                Id = builder.Id.Value,
                ImageLoader = builder.ImageLoader.Value,
                ImageSource = builder.ImageSource.Value
            };

            _unhandledImageInfos.Add(info.Id, info);
            return InsertInfoAndCreateGeneric(info, StepType.Image);
        }

        private async Task<SQLiteStepInternalInfo> InsertInfoAndCreateGeneric<T>(T info, StepType type)
            where T : IStorable
        {
            await Connection.InsertAsync(info);
            return CreateGenericInfo(info.Id, type);
        }

        private static SQLiteStepInternalInfo CreateGenericInfo(Guid id, StepType type) =>
            new SQLiteStepInternalInfo
            {
                Id = id,
                Type = type
            };

        #endregion
    }
}
