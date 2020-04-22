namespace CooKit.Models.Pictograms
{
    public interface IPictogram : IEntity
    {
        string Name { get; set; }
        string Description { get; set; }
        string Icon { get; set; }
    }
}
