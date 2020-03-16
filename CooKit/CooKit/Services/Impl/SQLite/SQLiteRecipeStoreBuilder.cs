using CooKit.Models;
using CooKit.Models.Impl;
using System;
using System.Threading.Tasks;
using SQLite;

namespace CooKit.Services.Impl.SQLite
{
    public sealed class SQLiteRecipeStoreBuilder : IAsyncBuilder<IRecipeStore>
    {
        public IBuilderProperty<SQLiteRecipeStoreBuilder, IImageStore> ImageStore { get; }
        public IBuilderProperty<SQLiteRecipeStoreBuilder, IIngredientStore> IngredientStore { get; }
        public IBuilderProperty<SQLiteRecipeStoreBuilder, IPictogramStore> PictogramStore { get; }
        public IBuilderProperty<SQLiteRecipeStoreBuilder, IRecipeStepStore> RecipeStepStore { get; }
        public IBuilderProperty<SQLiteRecipeStoreBuilder, SQLiteAsyncConnection> DatabaseConnection { get; }

        public SQLiteRecipeStoreBuilder()
        {
            ImageStore = new BuilderPropertyImpl<SQLiteRecipeStoreBuilder, IImageStore>(this);
            IngredientStore = new BuilderPropertyImpl<SQLiteRecipeStoreBuilder, IIngredientStore>(this);
            PictogramStore = new BuilderPropertyImpl<SQLiteRecipeStoreBuilder, IPictogramStore>(this);
            RecipeStepStore = new BuilderPropertyImpl<SQLiteRecipeStoreBuilder, IRecipeStepStore>(this);
            DatabaseConnection = new BuilderPropertyImpl<SQLiteRecipeStoreBuilder, SQLiteAsyncConnection>(this);
        }

        public Task<IRecipeStore> BuildAsync()
        {
            if (ImageStore.Value is null)
                throw new ArgumentNullException(nameof(ImageStore));

            if (IngredientStore.Value is null)
                throw new ArgumentNullException(nameof(IngredientStore));

            if (PictogramStore.Value is null)
                throw new ArgumentNullException(nameof(PictogramStore));

            if (RecipeStepStore.Value is null)
                throw new ArgumentNullException(nameof(RecipeStepStore));

            if (DatabaseConnection.Value is null)
                throw new ArgumentNullException(nameof(DatabaseConnection));

            var store = new SQLiteRecipeStore(DatabaseConnection.Value, ImageStore.Value, 
                IngredientStore.Value, PictogramStore.Value, RecipeStepStore.Value);

            return store
                .InitAsync()
                .ContinueWith(_ => (IRecipeStore) store);
        }
    }
}
