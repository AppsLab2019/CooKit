using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CooKit.Models.Impl
{
    internal sealed class RecipeImpl : IRecipe
    {
        public Guid Id { get; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public ImageSource MainImage { get; internal set; }

        public IReadOnlyList<IPictogram> Pictograms { get; internal set; }
        public IReadOnlyList<IIngredient> Ingredients { get; internal set; }
        public IReadOnlyList<string> Steps { get; internal set; }

        internal RecipeImpl(Guid id) =>
            Id = id;
    }
}
