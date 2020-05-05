using System;
using System.Collections.Generic;
using CooKit.Models.Ingredients;
using CooKit.Models.Pictograms;

namespace CooKit.Models.Recipes
{
    public sealed class Recipe : IRecipe
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int EstimatedTime { get; set; }

        public bool IsFavorite { get; set; }

        public string PreviewImage { get; set; }
        public IList<string> Images { get; set; }

        public IList<IIngredient> Ingredients { get; set; }
        public IList<IPictogram> Pictograms { get; set; }
        //public IList<IStep> Steps { get; set; }
    }
}
