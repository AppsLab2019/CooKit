using CooKit.Models.Recipes;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CooKit.ViewModels.Recipes
{
    public class RecipeIntroductionViewModel
    {

        private readonly IRecipe recipe_;

        public string Name { get { return recipe_.Name; } }

        public RecipeIntroductionViewModel([NotNull] IRecipe recipe)
        {
            recipe_ = recipe;
        }

    }
}
