using System;
using Xamarin.Forms;

namespace CooKit.Models.Impl.Generic
{
    internal sealed class GenericPictogram : IPictogram
    {
        public Guid Id { get; internal set; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public ImageSource Image { get; internal set; }
    }
}
