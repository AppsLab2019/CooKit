using System.Collections.Generic;
using CooKit.Models.Ingredients;
using CooKit.Models.Pictograms;

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

        IList<IIngredient> Ingredients { get; set; }
        IList<IPictogram> Pictograms { get; set; }
        //IList<IStep> Steps { get; set; }
    }
}
