namespace CooKit.Models.Steps
{
    public interface IStep : IStorable
    {
        StepType Type { get; }
    }
}
