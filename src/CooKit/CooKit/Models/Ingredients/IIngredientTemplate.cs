using CooKit.Models.Units;

namespace CooKit.Models.Ingredients
{
    public interface IIngredientTemplate : IEntity
    {
        string Name { get; set; }
        string Icon { get; set; }
        UnitCategory UnitCategory { get; set; }
    }
}
