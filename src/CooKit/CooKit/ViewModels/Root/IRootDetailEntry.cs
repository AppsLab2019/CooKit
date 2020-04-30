using System;

namespace CooKit.ViewModels.Root
{
    // TODO: move me
    public interface IRootDetailEntry
    {
        string Icon { get; set; }
        string Text { get; set; }
        Type ViewModelType { get; set; }
    }
}
