using System;
using System.Collections.Generic;
using CooKit.Models.Steps;
using Xamarin.Forms;

namespace CooKit.Models.Impl.Generic
{
    internal sealed class GenericRecipe : IRecipe
    {
        public Guid Id { get; internal set; }

        public string Name { get; internal set; }
        public string Description { get; internal set; }

        public ImageSource Image { get; internal set; }
        public TimeSpan RequiredTime { get; internal set; }

        public IReadOnlyList<IIngredient> Ingredients { get; internal set; }
        public IReadOnlyList<IPictogram> Pictograms { get; internal set; }
        public IReadOnlyList<IStep> Steps { get; internal set; }
    }
}
