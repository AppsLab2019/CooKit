using System;
using System.Threading.Tasks;
using CooKit.Models;
using CooKit.Models.Impl;
using CooKit.Models.Impl.Generic;
using CooKit.Models.Impl.SQLite;
using SQLite;
using Xamarin.Forms;

namespace CooKit.Services.Impl.SQLite
{
    internal sealed class SQLitePictogramStore : SQLiteStoreBase<IPictogram, IPictogramBuilder, SQLitePictogramInternalInfo>, IPictogramStore
    {
        private readonly ImageSource _defaultIcon;

        internal SQLitePictogramStore(SQLiteAsyncConnection connection, IImageStore imageStore) : base(connection, imageStore)
        {
            // TODO: replace with default icon
            _defaultIcon = null;
        }

        public override IPictogramBuilder CreateBuilder() =>
            new StoreCallbackPictogramBuilder(this);

        private protected override async Task<IPictogram> InternalInfoToObject(SQLitePictogramInternalInfo info)
        {
            if (info is null)
                throw new ArgumentNullException(nameof(info));

            return new GenericPictogram
            {
                Id = info.Id,
                Name = info.Name,
                Description = info.Description,
                Icon = await SafeImageLoadAsync(info.ImageLoader, info.ImageSource, _defaultIcon)
            };
        }

        private protected override Task<SQLitePictogramInternalInfo> BuilderToInternalInfo(IPictogramBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            var info = new SQLitePictogramInternalInfo
            {
                Id = builder.Id.Value,
                Name = builder.Name.Value,
                Description = builder.Description.Value,
                ImageLoader = builder.ImageLoader.Value,
                ImageSource = builder.ImageSource.Value
            };

            return Task.FromResult(info);
        }
    }
}
