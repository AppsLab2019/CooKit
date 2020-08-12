using CooKit.Mobile.Factories.Page;
using Xamarin.Forms;

namespace CooKit.Mobile.Extensions
{
    public static class PageFactoryExtensions
    {
        public static Page CreatePage<T>(this IPageFactory pageFactory) where T : Page
        {
            return pageFactory.CreatePage(typeof(T));
        }
    }
}
