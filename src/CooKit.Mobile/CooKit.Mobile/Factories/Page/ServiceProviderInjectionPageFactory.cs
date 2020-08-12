using System;
using System.Diagnostics;
using CooKit.Mobile.Services.Injector;

namespace CooKit.Mobile.Factories.Page
{
    // TODO: add logging
    public class ServiceProviderInjectionPageFactory : IPageFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IViewModelInjector _injector;

        public ServiceProviderInjectionPageFactory(IServiceProvider serviceProvider, IViewModelInjector injector)
        {
            _serviceProvider = serviceProvider;
            _injector = injector;
        }

        // TODO: make this more readable
        public Xamarin.Forms.Page CreatePage(Type pageType)
        {
            if (pageType == null)
                throw new ArgumentNullException(nameof(pageType));

            Debug.Assert(typeof(Xamarin.Forms.Page).IsAssignableFrom(pageType));

            var page = _serviceProvider.GetService(pageType) as Xamarin.Forms.Page;
            Debug.Assert(page != null);

            _injector.InjectViewModel(page);
            return page;
        }
    }
}
