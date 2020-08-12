using System;

namespace CooKit.Mobile.Factories.Page
{
    public interface IPageFactory
    {
        Xamarin.Forms.Page CreatePage(Type pageType);
    }
}
