namespace CooKit.Models.Units
{
    public interface IUnitInfo
    {
        string Name { get; }
        string Abbreviation { get; }
        float Multiplier { get; }
        UnitCategory Category { get; }
    }
}
