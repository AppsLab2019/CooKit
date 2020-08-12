using System;
using System.Collections.Generic;
using CooKit.Mobile.Models.Images;
using CooKit.Mobile.Models.Ingredients;
using CooKit.Mobile.Models.Steps;

namespace CooKit.Mobile.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan EstimatedTime { get; set; }

        public Image PreviewImage { get; set; }
        public IList<Image> Images { get; set; }

        public IList<Pictogram> Pictograms { get; set; }
        public IList<Ingredient> Ingredients { get; set; }
        public IList<Step> Steps { get; set; }
    }
}
