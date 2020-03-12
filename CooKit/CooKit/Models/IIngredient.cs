using System;
using Xamarin.Forms;

namespace CooKit.Models
{
    public interface IIngredient
    {
        Guid Id { get; }
        string Name { get; }
        ImageSource Icon { get; }
    }
}
