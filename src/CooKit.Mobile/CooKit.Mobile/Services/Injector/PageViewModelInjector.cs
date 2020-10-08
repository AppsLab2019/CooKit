using System;
using CooKit.Mobile.Registries.PageViewmodel;
using CooKit.Mobile.Viewmodels;
using Microsoft.Extensions.Logging;
using Xamarin.Forms;

namespace CooKit.Mobile.Services.Injector
{
    public class PageViewModelInjector : IViewModelInjector
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPageViewmodelTypeRegistry _typeRegistry;
        private readonly ILogger<PageViewModelInjector> _logger;

        public PageViewModelInjector(IServiceProvider serviceProvider, IPageViewmodelTypeRegistry typeRegistry, ILogger<PageViewModelInjector> logger)
        {
            _serviceProvider = serviceProvider;
            _typeRegistry = typeRegistry;
            _logger = logger;
        }

        public void InjectViewModel(BindableObject bindableObject)
        {
            if (!(bindableObject is Page page))
                return;

            var pageType = page.GetType();
            var viewModelType = _typeRegistry.GetViewmodelType(pageType);

            if (viewModelType == null)
                return;

            var viewModel = _serviceProvider.GetService(viewModelType);

            if (viewModel == null)
                return;

            // TODO: refactor this out
            if (viewModel is BaseViewmodel baseViewModel)
                baseViewModel.SetServiceProvider(_serviceProvider);

            bindableObject.BindingContext = viewModel;
            LogInjection(page, viewModel);
        }

        private void LogInjection(Page page, object viewModel)
        {
            var pageName = page.GetType().Name;
            var viewModelName = viewModel.GetType().Name;

            _logger.LogDebug($"Successfully injected {viewModelName} into {pageName}!");
        }
    }
}
