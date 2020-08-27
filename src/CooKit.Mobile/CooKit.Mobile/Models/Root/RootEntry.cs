using System;
using CooKit.Mobile.Models.Images;

namespace CooKit.Mobile.Models.Root
{
    public class RootEntry : IRootEntry
    {
        public Image Icon { get; set; }
        public string Title { get; set; }
        public Type ViewmodelType { get; set; }

        public RootEntry()
        {
        }

        public RootEntry(Image icon, string title, Type viewmodelType)
        {
            Icon = icon;
            Title = title;
            ViewmodelType = viewmodelType;
        }
    }
}
