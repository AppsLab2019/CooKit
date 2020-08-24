using System;
using Xamarin.Forms;

namespace CooKit.Mobile.Factories.Views
{
    public interface IViewFactory
    {
        View CreateView(Type viewType);
    }
}
