using System;
using System.Collections.Generic;
using CooKit.Models;

namespace CooKit.ViewModels.Recipes
{
    public sealed class RecipeViewModel
    {
        public string Name { get; }
        public string Description { get; }
        public IEnumerable<Uri> Images { get; }
        public IEnumerable<Pictogram> Pictograms { get; }
        public IEnumerable<Ingredient> Ingredients { get; }

        public RecipeViewModel()
        {
            Name = "Test Recipe";
            Description = "Lorem ipsum atd";
            Images = new[]
            {
                new Uri("https://blogs.biomedcentral.com/on-medicine/wp-content/uploads/sites/6/2019/09/iStock-1131794876.t5d482e40.m800.xtDADj9SvTVFjzuNeGuNUUGY4tm5d6UGU5tkKM0s3iPk-620x342.jpg"),
                new Uri("https://img-s-msn-com.akamaized.net/tenant/amp/entityid/AABHnbv.img?h=552&w=750&m=6&q=60&u=t&o=f&l=f&x=1163&y=707"),
                new Uri("https://img-s-msn-com.akamaized.net/tenant/amp/entityid/AABH94v.img?h=416&w=799&m=6&q=60&u=t&o=f&l=f&x=432&y=390"),
                new Uri("https://img-s-msn-com.akamaized.net/tenant/amp/entityid/AABHio8.img?h=416&w=799&m=6&q=60&u=t&o=f&l=f")
            };

            Pictograms = null;
            Ingredients = null;
        }
    }
}
