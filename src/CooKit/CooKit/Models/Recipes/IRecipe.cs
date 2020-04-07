using System;
using System.Collections.Generic;

namespace CooKit.Models.Recipes
{
    public interface IRecipe : IEntity
    {
        string Name { get; set; }
        string Description { get; set; }
        
        Uri PreviewImage { get; set; }
        IList<Uri> Images { get; set; }

        IList<Guid> PictogramIds { get; set; }
        IList<Guid> IngredientIds { get; set; }
        IList<Guid> StepIds { get; set; }
    }
}
