using CooKit.Models;
using CooKit.Models.Impl;
using System;
using System.Threading.Tasks;

namespace CooKit.Services.Impl.SQLite
{
    public sealed class SQLiteRecipeStoreBuilder
    {
        public IBuilderProperty<SQLiteRecipeStoreBuilder, IImageStore> ImageStore { get; }

        public SQLiteRecipeStoreBuilder()
        {
            ImageStore = new BuilderPropertyImpl<SQLiteRecipeStoreBuilder, IImageStore>(this);
        }

        public async Task<IRecipeStore> BuildAsync()
        {
            if (ImageStore.Value is null)
                throw new ArgumentException(nameof(ImageStore));

            var store = new SQLiteRecipeStore("placeholder", ImageStore.Value);
            await store.LoadRecipesAsync();

            return store;
        }
    }
}
