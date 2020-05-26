using System;

namespace CooKit.Models.Ingredients
{
    public interface IIngredientDto
    {
        Guid TemplateId { get; set; }
        string Note { get; set; }
        float Quantity { get; set; }
    }
}
