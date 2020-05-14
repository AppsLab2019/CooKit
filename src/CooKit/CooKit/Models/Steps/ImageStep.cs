using System;

namespace CooKit.Models.Steps
{
    public sealed class ImageStep : IImageStep
    {
        public Guid Id { get; set; }
        public string Image { get; set; }
    }
}
