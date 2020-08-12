using System;
using CooKit.Mobile.Models.Images;

namespace CooKit.Mobile.Models.Root
{
    public interface IRootViewmodelEntry
    {
        Image Icon { get; }
        string Title { get; }
        Type ViewmodelType { get; }
    }
}
