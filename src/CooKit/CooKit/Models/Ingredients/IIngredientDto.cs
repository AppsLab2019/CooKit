using System;

namespace CooKit.Models.Ingredients
{
    public interface IIngredientDto : IEntity
    {
        Guid TemplateId { get; set; }
        string Note { get; set; }
        float Quantity { get; set; }
    }
}
