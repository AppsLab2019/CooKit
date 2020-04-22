using System;
using System.Collections.Generic;

namespace CooKit.Models.Recipes
{
    public interface IRecipe : IEntity
    {
        string Name { get; set; }
        string Description { get; set; }
        int EstimatedTime { get; set; }

        bool IsFavorite { get; set; }

        string PreviewImage { get; set; }
        //IList<string> Images { get; set; }

        IList<Guid> IngredientIds { get; set; }
        IList<Guid> PictogramIds { get; set; }
    }
}
