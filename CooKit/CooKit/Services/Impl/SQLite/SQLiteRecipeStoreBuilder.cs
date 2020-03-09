using CooKit.Models;
using CooKit.Models.Impl;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CooKit.Services.Impl.SQLite
{
    public sealed class SQLiteRecipeStoreBuilder
    {
        public IBuilderProperty<SQLiteRecipeStoreBuilder, IImageStore> ImageStore { get; }
        public IBuilderProperty<SQLiteRecipeStoreBuilder, string> DatabasePath { get; }

        public SQLiteRecipeStoreBuilder()
        {
            ImageStore = new BuilderPropertyImpl<SQLiteRecipeStoreBuilder, IImageStore>(this);
            DatabasePath = new BuilderPropertyImpl<SQLiteRecipeStoreBuilder, string>(this,
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CooKit.db3"));
        }

        public async Task<IRecipeStore> BuildAsync()
        {
            if (ImageStore.Value is null)
                throw new ArgumentNullException(nameof(ImageStore));

            if (DatabasePath.Value is null)
                throw new ArgumentNullException(nameof(DatabasePath));

            var store = new SQLiteRecipeStore(DatabasePath.Value, ImageStore.Value);
            await store.LoadRecipesAsync();

            return store;
        }
    }
}
