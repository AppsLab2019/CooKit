using System;
using System.Collections.Generic;
using System.Reflection;
using CooKit.Attributes;
using Xamarin.Forms;
using XF.Material.Forms.UI;

namespace CooKit.Factories.Pages
{
    public sealed class PageFactory : IPageFactory
    {
        private readonly IDictionary<Type, Page> _keptPages;
        private readonly IDictionary<Type, Page> _keptRootPages;

        public PageFactory()
        {
            _keptPages = new Dictionary<Type, Page>();
            _keptRootPages = new Dictionary<Type, Page>();
        }

        public Page CreatePage(Type viewType)
        {
            return InternalCreatePage(viewType, _keptPages, ActivatePage);
        }

        public Page CreateRootPage(Type viewType)
        {
            return InternalCreatePage(viewType, _keptRootPages, type =>
            {
                var page = ActivatePage(type);
                return new MaterialNavigationPage(page);
            });
        }

        private static Page InternalCreatePage(Type viewType, IDictionary<Type, Page> keptPages, Func<Type, Page> createFunc)
        {
            if (viewType is null)
                throw new ArgumentNullException(nameof(viewType));

            if (keptPages.TryGetValue(viewType, out var keptPage))
                return keptPage;

            var page = createFunc(viewType);

            if (viewType.GetCustomAttribute<KeepPageAttribute>() is null)
                return page;

            keptPages[viewType] = page;
            return page;
        }

        private static Page ActivatePage(Type type)
        {
            ThrowIfNotPageType(type);
            return (Page) Activator.CreateInstance(type);
        }

        private static void ThrowIfNotPageType(Type type)
        {
            if (!typeof(Page).IsAssignableFrom(type))
                throw new ArgumentException($"Type {type.Name} is not a Page!");
        }
    }
}
