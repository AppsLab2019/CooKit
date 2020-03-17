namespace CooKit.Models.Steps
{
    public interface IBigImageRecipeStepBuilder : IRecipeStepBuilder
    {
        IBuilderProperty<IBigImageRecipeStepBuilder, string> ImageLoader { get; }
        IBuilderProperty<IBigImageRecipeStepBuilder, string> ImageSource { get; }
    }
}
