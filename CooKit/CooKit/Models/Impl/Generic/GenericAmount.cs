using CooKit.Models.Units;

namespace CooKit.Models.Impl.Generic
{
    internal sealed class GenericAmount : IAmount
    {
        public float BaseValue { get; set; }
        public IUnit Unit { get; set; }

        public float Value => BaseValue * Unit.BaseMultiplier;
    }
}
