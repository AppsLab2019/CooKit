using System;

namespace CooKit.Models.Steps
{
    public interface IRecipeStepBuilder : IAsyncBuilder<IRecipeStep>
    {
        IBuilderProperty<IRecipeStepBuilder, Guid> Id { get; }

        ITextRecipeStepBuilder ToTextBuilder();
    }
}
