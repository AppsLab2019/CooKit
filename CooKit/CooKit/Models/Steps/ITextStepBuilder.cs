namespace CooKit.Models.Steps
{
    public interface ITextStepBuilder : IStepBuilder
    {
        IBuilderProperty<ITextStepBuilder, string> Text { get; }
    }
}
