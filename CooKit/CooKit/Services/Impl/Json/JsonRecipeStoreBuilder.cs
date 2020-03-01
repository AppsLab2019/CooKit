using System;
using CooKit.Models;
using CooKit.Models.Impl;

namespace CooKit.Services.Impl.Json
{
    public sealed class JsonRecipeStoreBuilder
    {
        public IBuilderProperty<JsonRecipeStoreBuilder, IJsonStore> JsonStore { get; }
        public IBuilderProperty<JsonRecipeStoreBuilder, IImageStore> ImageStore { get; }
        public IBuilderProperty<JsonRecipeStoreBuilder, string> RecipeIdsJson { get; }

        public JsonRecipeStoreBuilder()
        {
            JsonStore = new BuilderPropertyImpl<JsonRecipeStoreBuilder, IJsonStore>(this);
            ImageStore = new BuilderPropertyImpl<JsonRecipeStoreBuilder, IImageStore>(this);
            RecipeIdsJson = new BuilderPropertyImpl<JsonRecipeStoreBuilder, string>(this);
        }

        // TODO: replace with appropriate exceptions
        public IRecipeStore Build()
        {
            if (JsonStore.Value is null)
                throw new Exception();

            if (ImageStore.Value is null)
                throw new Exception();

            if (RecipeIdsJson.Value is null)
                throw new Exception();

            return new JsonRecipeStore(
                JsonStore.Value,
                ImageStore.Value);
        }
    }
}
