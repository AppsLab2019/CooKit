using System;

namespace CooKit.Mobile.Factories.Page.Root
{
    public interface IRootPageFactory : IPageFactory
    {
        Xamarin.Forms.NavigationPage CreateRootPage(Type pageType);
    }
}
