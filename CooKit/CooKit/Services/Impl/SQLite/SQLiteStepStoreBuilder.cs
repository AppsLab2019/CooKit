using System;
using System.Threading.Tasks;
using CooKit.Models;
using CooKit.Models.Impl;
using CooKit.Services.SQLite;
using SQLite;

namespace CooKit.Services.Impl.SQLite
{
    public sealed class SQLiteStepStoreBuilder : ISQLiteStepStoreBuilder
    {
        public IBuilderProperty<ISQLiteStepStoreBuilder, SQLiteAsyncConnection> Connection { get; }
        public IBuilderProperty<ISQLiteStepStoreBuilder, IImageStore> ImageStore { get; }

        public SQLiteStepStoreBuilder()
        {
            Connection = new BuilderPropertyImpl<ISQLiteStepStoreBuilder, SQLiteAsyncConnection>(this);
            ImageStore = new BuilderPropertyImpl<ISQLiteStepStoreBuilder, IImageStore>(this);
        }

        public async Task<IStepStore> BuildAsync()
        {
            if (Connection.Value is null)
                throw new ArgumentNullException(nameof(Connection));

            if (ImageStore.Value is null)
                throw new ArgumentNullException(nameof(ImageStore));

            var store = new SQLiteStepStore(Connection.Value, ImageStore.Value);

            await store.InitAsync();
            return store;
        }
    }
}
