using System.Collections.Generic;
using System.Linq;
using CooKit.Models.Units;

namespace CooKit.Services.Units
{
    public sealed class UnitService : IUnitService
    {
        private readonly IList<IUnitInfo> _units;

        public UnitService()
        {
            _units = CreateUnitsList();
        }

        public IList<IUnitInfo> GetAvailableUnits()
        {
            return _units.ToList();
        }

        public IList<IUnitInfo> GetAvailableUnits(UnitCategory category)
        {
            return _units
                .Where(unit => unit.Category == category)
                .ToList();
        }

        private static IList<IUnitInfo> CreateUnitsList()
        {
            return new List<IUnitInfo>
            {
                new UnitInfo("None", string.Empty, 1f, UnitCategory.None),

                new UnitInfo("Grams", "g", 1f, UnitCategory.Weight),

                new UnitInfo("Milliliter", "ml", 1f, UnitCategory.Volume),
                new UnitInfo("Liter", "l", 0.001f, UnitCategory.Volume)
            };
        }
    }
}
