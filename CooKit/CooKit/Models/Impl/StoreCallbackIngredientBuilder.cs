using System;
using CooKit.Services;

namespace CooKit.Models.Impl
{
    public sealed class StoreCallbackIngredientBuilder : IIngredientBuilder
    {
        public IBuilderProperty<IIngredientBuilder, Guid> Id { get; }
        public IBuilderProperty<IIngredientBuilder, string> Name { get; }

        public IBuilderProperty<IIngredientBuilder, string> ImageLoader { get; }
        public IBuilderProperty<IIngredientBuilder, string> ImageSource { get; }

        private readonly IIngredientStore _ingredientStore;

        public StoreCallbackIngredientBuilder(IIngredientStore ingredientStore)
        {
            _ingredientStore = ingredientStore;

            Id = new BuilderPropertyImpl<IIngredientBuilder, Guid>(this, Guid.NewGuid());
            Name = new BuilderPropertyImpl<IIngredientBuilder, string>(this);

            ImageLoader = new BuilderPropertyImpl<IIngredientBuilder, string>(this);
            ImageSource = new BuilderPropertyImpl<IIngredientBuilder, string>(this);
        }

        public IIngredient Build()
        {
            _ingredientStore.Add(this);
            return _ingredientStore.Load(Id.Value);
        }
    }
}
