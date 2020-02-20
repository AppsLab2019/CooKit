using System;

namespace CooKit.Services.Impl.Json
{
    internal class DeserializedRecipeInfo
    {
        internal string Name;
        internal string Description;

        internal Guid[] Ingredients;
        internal Guid[] Pictograms;
    }
}
