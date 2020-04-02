namespace CooKit.Models.Units
{
    public interface IUnit
    {
        string FullName { get; }
        string Abbreviation { get; }

        UnitType Type { get; }
        float BaseMultiplier { get; }
    }
}
