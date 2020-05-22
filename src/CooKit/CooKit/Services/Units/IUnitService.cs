using System.Collections.Generic;
using CooKit.Models.Units;

namespace CooKit.Services.Units
{
    public interface IUnitService
    {
        IList<IUnitInfo> GetAvailableUnits();
        IList<IUnitInfo> GetAvailableUnits(UnitCategory category);
    }
}
