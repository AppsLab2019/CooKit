using System;
using System.Collections.Generic;
using CooKit.Mobile.Extensions;

namespace CooKit.Mobile.Registries.PageViewmodel
{
    public class PageViewmodelTypeRegistry : IPageViewmodelTypeRegistry
    {
        private readonly IDictionary<Type, Type> _pageToViewmodel;
        private readonly IDictionary<Type, Type> _viewmodelToPage;

        public PageViewmodelTypeRegistry()
        {
            _pageToViewmodel = new Dictionary<Type, Type>();
            _viewmodelToPage = new Dictionary<Type, Type>();
        }

        public void Register(Type pageType, Type viewmodelType)
        {
            if (pageType == null)
                throw new ArgumentNullException(nameof(pageType));

            if (viewmodelType == null)
                throw new ArgumentNullException(nameof(viewmodelType));

            // TODO: check if already registered
            _pageToViewmodel[pageType] = viewmodelType;
            _viewmodelToPage[viewmodelType] = pageType;
        }

        public Type GetViewmodelType(Type pageType)
        {
            return _pageToViewmodel.GetOrDefault(pageType);
        }

        public Type GetPageType(Type viewmodelType)
        {
            return _viewmodelToPage.GetOrDefault(viewmodelType);
        }
    }
}
