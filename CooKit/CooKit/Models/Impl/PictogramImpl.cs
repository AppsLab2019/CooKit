using System;
using Xamarin.Forms;

namespace CooKit.Models.Impl
{
    internal sealed class PictogramImpl : IPictogram
    {
        public Guid Id { get; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public ImageSource Image { get; internal set; }

        internal PictogramImpl(Guid id, string name = default, 
            string description = default, ImageSource image = default)
        {
            Id = id;
            Name = name;
            Description = description;
            Image = image;
        }
    }
}
