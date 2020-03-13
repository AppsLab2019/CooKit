using CooKit.Models;
using CooKit.Models.Impl;
using System;
using System.Threading.Tasks;
using SQLite;

namespace CooKit.Services.Impl.SQLite
{
    public sealed class SQLiteRecipeStoreBuilder
    {
        public IBuilderProperty<SQLiteRecipeStoreBuilder, IImageStore> ImageStore { get; }
        public IBuilderProperty<SQLiteRecipeStoreBuilder, IIngredientStore> IngredientStore { get; }
        public IBuilderProperty<SQLiteRecipeStoreBuilder, IPictogramStore> PictogramStore { get; }
        public IBuilderProperty<SQLiteRecipeStoreBuilder, SQLiteAsyncConnection> DatabaseConnection { get; }

        public SQLiteRecipeStoreBuilder()
        {
            ImageStore = new BuilderPropertyImpl<SQLiteRecipeStoreBuilder, IImageStore>(this);
            IngredientStore = new BuilderPropertyImpl<SQLiteRecipeStoreBuilder, IIngredientStore>(this);
            PictogramStore = new BuilderPropertyImpl<SQLiteRecipeStoreBuilder, IPictogramStore>(this);
            DatabaseConnection = new BuilderPropertyImpl<SQLiteRecipeStoreBuilder, SQLiteAsyncConnection>(this);
        }

        public async Task<IRecipeStore> BuildAsync()
        {
            if (ImageStore.Value is null)
                throw new ArgumentNullException(nameof(ImageStore));

            if (IngredientStore.Value is null)
                throw new ArgumentNullException(nameof(IngredientStore));

            if (PictogramStore.Value is null)
                throw new ArgumentNullException(nameof(PictogramStore));

            if (DatabaseConnection.Value is null)
                throw new ArgumentNullException(nameof(DatabaseConnection));

            var store = new SQLiteRecipeStore(DatabaseConnection.Value, 
                ImageStore.Value, IngredientStore.Value, PictogramStore.Value);
            await store.InitAsync();

            return store;
        }
    }
}
