using System;

namespace CooKit.Models.Ingredients
{
    public sealed class IngredientDto : IIngredientDto
    {
        public Guid TemplateId { get; set; }
        public string Note { get; set; }
        public float Quantity { get; set; }
    }
}
