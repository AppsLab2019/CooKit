using System;
using System.Threading.Tasks;
using CooKit.Models;
using CooKit.Models.Impl;
using CooKit.Services.SQLite;
using SQLite;

namespace CooKit.Services.Impl.SQLite
{
    public sealed class SQLiteRecipeStoreBuilder : ISQLiteRecipeStoreBuilder
    {
        public IBuilderProperty<ISQLiteRecipeStoreBuilder, SQLiteAsyncConnection> Connection { get; }
        public IBuilderProperty<ISQLiteRecipeStoreBuilder, IImageStore> ImageStore { get; }
        public IBuilderProperty<ISQLiteRecipeStoreBuilder, IIngredientStore> IngredientStore { get; }
        public IBuilderProperty<ISQLiteRecipeStoreBuilder, IPictogramStore> PictogramStore { get; }
        public IBuilderProperty<ISQLiteRecipeStoreBuilder, IStepStore> StepStore { get; }

        public SQLiteRecipeStoreBuilder()
        {
            Connection = new BuilderPropertyImpl<ISQLiteRecipeStoreBuilder, SQLiteAsyncConnection>(this);
            ImageStore = new BuilderPropertyImpl<ISQLiteRecipeStoreBuilder, IImageStore>(this);
            IngredientStore = new BuilderPropertyImpl<ISQLiteRecipeStoreBuilder, IIngredientStore>(this);
            PictogramStore = new BuilderPropertyImpl<ISQLiteRecipeStoreBuilder, IPictogramStore>(this);
            StepStore = new BuilderPropertyImpl<ISQLiteRecipeStoreBuilder, IStepStore>(this);
        }

        public async Task<IRecipeStore> BuildAsync()
        {
            if (Connection.Value is null)
                throw new ArgumentNullException(nameof(Connection));

            if (ImageStore.Value is null)
                throw new ArgumentNullException(nameof(ImageStore));

            if (IngredientStore.Value is null)
                throw new ArgumentNullException(nameof(IngredientStore));

            if (PictogramStore.Value is null)
                throw new ArgumentNullException(nameof(PictogramStore));

            if (StepStore.Value is null)
                throw new ArgumentNullException(nameof(StepStore));

            var store = new SQLiteRecipeStore(Connection.Value, ImageStore.Value,
                IngredientStore.Value, PictogramStore.Value, StepStore.Value);

            await store.InitAsync();
            return store;
        }
    }
}
