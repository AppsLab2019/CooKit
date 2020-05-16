using System;

namespace CooKit.ViewModels.Root
{
    public sealed class RootDetailEntry : IRootDetailEntry
    {
        public string Icon { get; set; }
        public string Text { get; set; }
        public Type ViewModelType { get; set; }
    }
}
