using System;
using XF.Material.Forms.UI;

namespace CooKit.Mobile.Factories.Page.Root
{
    // TODO: add logging
    public class MaterialRootPageFactory : IRootPageFactory
    {
        private readonly IPageFactory _pageFactory;

        public MaterialRootPageFactory(IPageFactory pageFactory)
        {
            _pageFactory = pageFactory;
        }

        public Xamarin.Forms.Page CreatePage(Type pageType)
        {
            return CreateRootPage(pageType);
        }

        public Xamarin.Forms.NavigationPage CreateRootPage(Type pageType)
        {
            var page = _pageFactory.CreatePage(pageType);

            return page != null ? new MaterialNavigationPage(page) : null;
        }
    }
}
