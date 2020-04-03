namespace CooKit.Models.Units
{
    public interface IUnit
    {
        string FullName { get; }
        string Abbreviation { get; }

        UnitCategory Category { get; }
        float BaseMultiplier { get; }
        bool IsConvertible { get; }
    }
}
