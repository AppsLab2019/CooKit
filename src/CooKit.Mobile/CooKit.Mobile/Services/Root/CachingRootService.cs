using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using CooKit.Mobile.Extensions;
using CooKit.Mobile.Factories.Page.Root;
using CooKit.Mobile.Providers.Page.MasterDetail;
using CooKit.Mobile.Registries.PageViewmodel;
using Microsoft.Extensions.Logging;
using Xamarin.Forms;

namespace CooKit.Mobile.Services.Root
{
    public class CachingRootService : IRootService
    {
        private readonly IPageViewmodelTypeRegistry _registry;
        private readonly IRootPageFactory _rootPageFactory;
        private readonly IMasterDetailPageProvider _masterDetailPageProvider;
        private readonly ILogger<CachingRootService> _logger;
        private readonly IDictionary<Type, Page> _cachedRoots;

        public CachingRootService(IPageViewmodelTypeRegistry registry, IRootPageFactory rootPageFactory, 
            IMasterDetailPageProvider masterDetailPageProvider, ILogger<CachingRootService> logger)
        {
            _registry = registry;
            _rootPageFactory = rootPageFactory;
            _masterDetailPageProvider = masterDetailPageProvider;
            _logger = logger;
            _cachedRoots = new Dictionary<Type, Page>();
        }

        public async Task SetRootAsync(Type viewmodelType)
        {
            if (viewmodelType == null)
                throw new ArgumentNullException(nameof(viewmodelType));

            var rootPage = await GetRootPage(viewmodelType);
            var masterDetailPage = _masterDetailPageProvider.GetMasterDetailPage();
            masterDetailPage.Detail = rootPage;

            // TODO: improve log message
            _logger.LogDebug("Changed root page!");
        }

        private async Task<Page> GetRootPage(Type viewmodelType)
        {
            if (_cachedRoots.TryGetValue(viewmodelType, out var cachedRootPage))
                return cachedRootPage;

            var pageType = _registry.GetPageType(viewmodelType);
            Debug.Assert(pageType != null);

            var rootPage = _rootPageFactory.CreateRootPage(pageType);
            Debug.Assert(rootPage != null);

            _cachedRoots[viewmodelType] = rootPage;
            await InitializeRootPageViewmodel(rootPage);
            return rootPage;
        }

        private static Task InitializeRootPageViewmodel(BindableObject rootPage)
        {
            if (!(rootPage is NavigationPage navigationPage))
                return rootPage.InitializeViewmodelAsync(null);

            var page = navigationPage.RootPage;
            return page.InitializeViewmodelAsync(null);
        }
    }
}
