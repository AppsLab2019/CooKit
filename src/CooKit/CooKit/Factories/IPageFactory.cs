using System;
using Xamarin.Forms;

namespace CooKit.Factories
{
    public interface IPageFactory
    {
        Page CreatePage(Type viewType);
        Page CreateRootPage(Type viewType);
    }
}
