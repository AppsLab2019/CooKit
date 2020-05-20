using System;

namespace CooKit.Models.RootDetails
{
    // TODO: move me
    public interface IRootDetailEntry
    {
        string Icon { get; set; }
        string Text { get; set; }
        Type ViewModelType { get; set; }
    }
}
