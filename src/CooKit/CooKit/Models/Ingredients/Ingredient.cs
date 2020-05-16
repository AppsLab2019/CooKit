using System;

namespace CooKit.Models.Ingredients
{
    public sealed class Ingredient : IIngredient
    {
        public Guid Id { get; set; }
        public IIngredientTemplate Template { get; set; }
        public string Note { get; set; }
        public float Quantity { get; set; }
    }
}
