using System;
using Xamarin.Forms;

namespace CooKit.Factories.Pages
{
    public interface IPageFactory
    {
        Page CreatePage(Type viewType);
        Page CreateRootPage(Type viewType);
    }
}
