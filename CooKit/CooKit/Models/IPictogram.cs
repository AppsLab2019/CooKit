using System;
using Xamarin.Forms;

namespace CooKit.Models
{
    public interface IPictogram
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get; }
        ImageSource Image { get; }
    }
}
