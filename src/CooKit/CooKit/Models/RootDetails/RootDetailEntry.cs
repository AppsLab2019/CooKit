using System;

namespace CooKit.Models.RootDetails
{
    public sealed class RootDetailEntry : IRootDetailEntry
    {
        public string Icon { get; set; }
        public string Text { get; set; }
        public Type ViewModelType { get; set; }
    }
}
