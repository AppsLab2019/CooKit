using System;
using CooKit.Models.Steps;
using Xamarin.Forms;

namespace CooKit.Models.Impl.Generic
{
    internal sealed class GenericImageStep : IBigImageRecipeStep
    {
        public Guid Id { get; internal set; }
        public RecipeStepType Type { get; internal set; }
        public ImageSource Image { get; internal set; }
    }
}
