using System;
using CooKit.Mobile.Models.Images;

namespace CooKit.Mobile.Models.Root
{
    public interface IRootEntry
    {
        Image Icon { get; }
        string Title { get; }
        Type ViewmodelType { get; }
    }
}
