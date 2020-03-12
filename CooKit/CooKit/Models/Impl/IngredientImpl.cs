using System;
using Xamarin.Forms;

namespace CooKit.Models.Impl
{
    internal sealed class IngredientImpl : IIngredient
    {
        public Guid Id { get; }
        public string Name { get; internal set; }
        public ImageSource Icon { get; internal set; }

        internal IngredientImpl(Guid id, string name = default, ImageSource icon = default)
        {
            Id = id;
            Name = name;
            Icon = icon;
        }
    }
}
