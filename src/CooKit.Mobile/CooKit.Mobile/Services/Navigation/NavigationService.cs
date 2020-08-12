using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CooKit.Mobile.Extensions;
using CooKit.Mobile.Factories.Page;
using CooKit.Mobile.Providers.Page.CurrentRoot;
using CooKit.Mobile.Registries.PageViewmodel;
using CooKit.Mobile.Viewmodels;
using Microsoft.Extensions.Logging;
using Xamarin.Forms;

namespace CooKit.Mobile.Services.Navigation
{
    // TODO: maybe refactor the page creation and injection logging out of this class ?
    public class NavigationService : INavigationService
    {
        private readonly ILogger<NavigationService> _logger;
        private readonly IPageViewmodelTypeRegistry _registry;
        private readonly IPageFactory _pageFactory;
        private readonly ICurrentRootPageProvider _rootPageProvider;

        public NavigationService(ILogger<NavigationService> logger, IPageViewmodelTypeRegistry registry, IPageFactory pageFactory, 
            ICurrentRootPageProvider rootPageProvider)
        {
            _logger = logger;
            _registry = registry;
            _pageFactory = pageFactory;
            _rootPageProvider = rootPageProvider;
        }

        public Task PushAsync(Type viewModelType)
        {
            return PushAsync(viewModelType, null);
        }

        public async Task PushAsync(Type viewModelType, object parameter)
        {
            if (viewModelType == null)
                throw new ArgumentNullException(nameof(viewModelType));

            var pageType = _registry.GetPageType(viewModelType);
            Debug.Assert(pageType != null);

            var page = _pageFactory.CreatePage(pageType);
            Debug.Assert(page != null);

            var initializationTask = page.InitializeViewmodelAsync(parameter);
            var navigationTask = InternalPushAsync(page);

            await Task.WhenAll(initializationTask, navigationTask);

            _logger.LogDebug($"Successfully pushed a page of type {pageType.Name}!");
        }

        private Task InternalPushAsync(Page page)
        {
            var navigationPage = GetRootPage();
            return navigationPage.PushAsync(page);
        }

        private NavigationPage GetRootPage()
        {
            var navigationPage = _rootPageProvider.GetCurrentRootPage();
            Debug.Assert(navigationPage != null);
            return navigationPage;
        }

        // TODO: take one more look at this logic
        // TODO: trigger viewmodel notification with back button press or remove it
        public async Task PopAsync()
        {
            var rootPage = _rootPageProvider.GetCurrentRootPage();
            var page = rootPage.CurrentPage;

            if (page.BindingContext is IViewmodel viewModel)
            {
                var shouldPop = await viewModel.OnExitAsync();

                // TODO: log this
                if (!shouldPop)
                    return;

                if (viewModel is IAsyncDisposable asyncDisposable)
                {
                    await asyncDisposable.DisposeAsync();
                }
                else if (viewModel is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            await rootPage.PopAsync();
            _logger.LogDebug($"Successfully popped a page of type {page.GetType().Name}");
        }
    }
}
