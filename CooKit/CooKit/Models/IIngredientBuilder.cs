using System;

namespace CooKit.Models
{
    public interface IIngredientBuilder : IAsyncBuilder<IIngredient>
    {
        IBuilderProperty<IIngredientBuilder, Guid> Id { get; }
        IBuilderProperty<IIngredientBuilder, string> Name { get; }

        IBuilderProperty<IIngredientBuilder, string> ImageLoader { get; }
        IBuilderProperty<IIngredientBuilder, string> ImageSource { get; }
    }
}
