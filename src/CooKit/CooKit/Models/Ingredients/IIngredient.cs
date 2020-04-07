using System;

namespace CooKit.Models.Ingredients
{
    public interface IIngredient
    {
        string Name { get; set; }

        Uri Icon { get; set; }
    }
}
