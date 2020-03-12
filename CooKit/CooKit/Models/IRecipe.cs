﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CooKit.Models
{
    public interface IRecipe
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get; }
        ImageSource MainImage { get; }

        IReadOnlyList<IPictogram> Pictograms { get; }

        IReadOnlyList<IIngredient> Ingredients { get; }
        IReadOnlyList<string> Steps { get; }
    }
}
