using System;

namespace CooKit.Models
{
    public interface IRecipeBuilder : IAsyncBuilder<IRecipe>
    {
        IBuilderProperty<IRecipeBuilder, Guid> Id { get; }
        IBuilderProperty<IRecipeBuilder, string> Name { get; }
        IBuilderProperty<IRecipeBuilder, string> Description { get; }
        IBuilderProperty<IRecipeBuilder, TimeSpan> RequiredTime { get; }

        IBuilderProperty<IRecipeBuilder, string> ImageLoader { get; }
        IBuilderProperty<IRecipeBuilder, string> ImageSource { get; }

        IBuilderProperty<IRecipeBuilder, Guid[]> IngredientIds { get; }
        IBuilderProperty<IRecipeBuilder, Guid[]> PictogramIds { get; }
        IBuilderProperty<IRecipeBuilder, Guid[]> StepIds { get; }
    }
}
