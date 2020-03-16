namespace CooKit.Models.Steps
{
    public interface ITextRecipeStepBuilder : IRecipeStepBuilder
    {
        IBuilderProperty<ITextRecipeStepBuilder, string> Text { get; }
    }
}
