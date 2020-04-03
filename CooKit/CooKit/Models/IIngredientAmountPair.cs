using CooKit.Models.Units;

namespace CooKit.Models
{
    public interface IIngredientAmountPair
    {
        IIngredient Ingredient { get; }
        IAmount Amount { get; }
    }
}
