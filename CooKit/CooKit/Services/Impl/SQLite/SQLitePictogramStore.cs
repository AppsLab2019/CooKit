using CooKit.Models;
using CooKit.Models.Impl;
using CooKit.Models.Impl.SQLite;
using SQLite;

namespace CooKit.Services.Impl.SQLite
{
    internal sealed class SQLitePictogramStore : SQLiteStoreBase<IPictogram, IPictogramBuilder, SQLitePictogram, SQLitePictogramInfo>, IPictogramStore
    {
        private readonly IImageStore _imageStore;

        internal SQLitePictogramStore(SQLiteAsyncConnection connection, IImageStore imageStore)
            : base(connection) => _imageStore = imageStore;

        public override IPictogramBuilder CreateBuilder() =>
            new StoreCallbackPictogramBuilder(this);

        protected internal override SQLitePictogram CreateObjectFromBuilder(IPictogramBuilder builder)
        {
            var info = new SQLitePictogramInfo
            {
                Id = builder.Id.Value,
                Name = builder.Name.Value,
                Description = builder.Description.Value,

                ImageSource = builder.ImageSource.Value,
                ImageLoader = builder.ImageLoader.Value
            };

            return new SQLitePictogram(info)
            {
                Image = _imageStore.LoadImage(info.ImageLoader, info.ImageSource)
            };
        }

        protected internal override SQLitePictogram CreateObjectFromInfo(SQLitePictogramInfo info) =>
            new SQLitePictogram(info)
            {
                Image = _imageStore.LoadImage(info.ImageLoader, info.ImageSource)
            };
    }
}
