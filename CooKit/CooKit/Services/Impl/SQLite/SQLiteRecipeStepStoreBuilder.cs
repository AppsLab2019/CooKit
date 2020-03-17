using System;
using System.Threading.Tasks;
using CooKit.Models;
using CooKit.Models.Impl;
using SQLite;

namespace CooKit.Services.Impl.SQLite
{
    public sealed class SQLiteRecipeStepStoreBuilder : IAsyncBuilder<IRecipeStepStore>
    {
        public IBuilderProperty<SQLiteRecipeStepStoreBuilder, SQLiteAsyncConnection> DatabaseConnection { get; }
        public IBuilderProperty<SQLiteRecipeStepStoreBuilder, IImageStore> ImageStore { get; }

        public SQLiteRecipeStepStoreBuilder()
        {
            DatabaseConnection = new BuilderPropertyImpl<SQLiteRecipeStepStoreBuilder, SQLiteAsyncConnection>(this);
            ImageStore = new BuilderPropertyImpl<SQLiteRecipeStepStoreBuilder, IImageStore>(this);
        }

        public Task<IRecipeStepStore> BuildAsync()
        {
            if (DatabaseConnection.Value is null)
                throw new ArgumentNullException(nameof(DatabaseConnection));

            var store = new SQLiteRecipeStepStore(DatabaseConnection.Value, ImageStore.Value);

            return store
                .InitAsync()
                .ContinueWith(_ => (IRecipeStepStore) store);
        }
    }
}
