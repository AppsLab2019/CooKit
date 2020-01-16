using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CooKit.Models.Recipes
{
    public interface IRecipe
    {

        string Name { get; }
        int Difficulty { get; }
        int TimeNeeded { get; }

        ImageSource HeaderImage { get; }
        string Description { get; }

    }
}