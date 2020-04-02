namespace CooKit.Models.Steps
{
    public interface IImageStepBuilder : IStepBuilder
    {
        IBuilderProperty<IImageStepBuilder, string> ImageLoader { get; }
        IBuilderProperty<IImageStepBuilder, string> ImageSource { get; }
    }
}
