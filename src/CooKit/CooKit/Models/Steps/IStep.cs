namespace CooKit.Models.Steps
{
    public interface IStep : IEntity
    {
        StepType Type { get; set; }
    }
}
