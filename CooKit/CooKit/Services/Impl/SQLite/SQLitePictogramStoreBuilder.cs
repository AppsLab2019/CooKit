using System;
using System.Threading.Tasks;
using CooKit.Models;
using CooKit.Models.Impl;
using CooKit.Services.SQLite;
using SQLite;

namespace CooKit.Services.Impl.SQLite
{
    public sealed class SQLitePictogramStoreBuilder : ISQLitePictogramStoreBuilder
    {
        public IBuilderProperty<ISQLitePictogramStoreBuilder, SQLiteAsyncConnection> Connection { get; }
        public IBuilderProperty<ISQLitePictogramStoreBuilder, IImageStore> ImageStore { get; }

        public SQLitePictogramStoreBuilder()
        {
            Connection = new BuilderPropertyImpl<ISQLitePictogramStoreBuilder, SQLiteAsyncConnection>(this);
            ImageStore = new BuilderPropertyImpl<ISQLitePictogramStoreBuilder, IImageStore>(this);
        }

        public Task<IPictogramStore> BuildAsync()
        {
            if (Connection.Value is null)
                throw new ArgumentNullException(nameof(Connection));

            if (ImageStore.Value is null)
                throw new ArgumentNullException(nameof(ImageStore));

            var store = new SQLitePictogramStore(Connection.Value, ImageStore.Value);
            return store.InitAsync().ContinueWith(_ => store as IPictogramStore);
        }
    }
}
