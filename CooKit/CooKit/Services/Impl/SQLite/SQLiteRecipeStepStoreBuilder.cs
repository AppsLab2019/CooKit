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

        public SQLiteRecipeStepStoreBuilder() =>
            DatabaseConnection = new BuilderPropertyImpl<SQLiteRecipeStepStoreBuilder, SQLiteAsyncConnection>(this);

        public Task<IRecipeStepStore> BuildAsync()
        {
            if (DatabaseConnection.Value is null)
                throw new ArgumentNullException(nameof(DatabaseConnection));

            var store = new SQLiteRecipeStepStore(DatabaseConnection.Value);

            return store
                .InitAsync()
                .ContinueWith(_ => (IRecipeStepStore) store);
        }
    }
}
