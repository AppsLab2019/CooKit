using System;
using CooKit.Models.Steps;

namespace CooKit.Models.Impl.Generic
{
    internal sealed class GenericTextStep : ITextRecipeStep
    {
        public Guid Id { get; set; }
        public RecipeStepType Type { get; set; }
        public string Text { get; set; }
    }
}
