using System;
using System.Threading.Tasks;
using CooKit.Models;
using CooKit.Models.Impl;
using CooKit.Services.SQLite;
using SQLite;

namespace CooKit.Services.Impl.SQLite
{
    public sealed class SQLiteIngredientStoreBuilder : ISQLiteIngredientStoreBuilder
    {
        public IBuilderProperty<ISQLiteIngredientStoreBuilder, SQLiteAsyncConnection> Connection { get; }
        public IBuilderProperty<ISQLiteIngredientStoreBuilder, IImageStore> ImageStore { get; }

        public SQLiteIngredientStoreBuilder()
        {
            Connection = new BuilderPropertyImpl<ISQLiteIngredientStoreBuilder, SQLiteAsyncConnection>(this);
            ImageStore = new BuilderPropertyImpl<ISQLiteIngredientStoreBuilder, IImageStore>(this);
        }

        public Task<IIngredientStore> BuildAsync()
        {
            if (Connection.Value is null)
                throw new ArgumentNullException(nameof(Connection));

            if (ImageStore.Value is null)
                throw new ArgumentNullException(nameof(ImageStore));

            var store = new SQLiteIngredientStore(Connection.Value, ImageStore.Value);

            return store.InitAsync().ContinueWith(_ => store as IIngredientStore);
        }
    }
}
