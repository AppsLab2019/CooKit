namespace CooKit.Models.Units
{
    public interface IAmount
    {
        float BaseValue { get; }
        float Value { get; }
        IUnit Unit { get; }
    }
}
