using System;
using System.Threading.Tasks;
using CooKit.Models;
using CooKit.Models.Impl;
using SQLite;

namespace CooKit.Services.Impl.SQLite
{
    public sealed class SQLitePictogramStoreBuilder
    {
        public IBuilderProperty<SQLitePictogramStoreBuilder, IImageStore> ImageStore { get; }
        public IBuilderProperty<SQLitePictogramStoreBuilder, SQLiteAsyncConnection> DatabaseConnection { get; }

        public SQLitePictogramStoreBuilder()
        {
            ImageStore = new BuilderPropertyImpl<SQLitePictogramStoreBuilder, IImageStore>(this);
            DatabaseConnection = new BuilderPropertyImpl<SQLitePictogramStoreBuilder, SQLiteAsyncConnection>(this);
        }

        public async Task<IPictogramStore> BuildAsync()
        {
            if (ImageStore.Value is null)
                throw new ArgumentNullException(nameof(ImageStore));

            if (DatabaseConnection.Value is null)
                throw new ArgumentNullException(nameof(DatabaseConnection));

            var store = new SQLitePictogramStore(DatabaseConnection.Value, ImageStore.Value);

            await store.InitAsync();
            return store;
        }
    }
}
