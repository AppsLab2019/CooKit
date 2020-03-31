using System;
using Xamarin.Forms;

namespace CooKit.Models.Impl.Generic
{
    internal sealed class GenericIngredient : IIngredient
    {
        public Guid Id { get; internal set; }
        public string Name { get; internal set; }
        public ImageSource Icon { get; internal set; }
    }
}
