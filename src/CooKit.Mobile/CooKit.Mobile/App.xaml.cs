using CooKit.Mobile.Converters;
using CooKit.Mobile.Extensions;
using CooKit.Mobile.Factories.Page;
using CooKit.Mobile.Pages;
using CooKit.Mobile.Providers.Page.MasterDetail;
using CooKit.Mobile.Services.Database;
using CooKit.Mobile.Services.Resources;
using Microsoft.Extensions.Logging;
using Xamarin.Forms;
using XF.Material.Forms;

namespace CooKit.Mobile
{
    public partial class App
    {
        private readonly ILogger<App> _logger;
        private readonly IDatabaseInitializationService _databaseInitializationService;
        private readonly IResourceInitializationService _resourceInitializationService;
        private readonly IPageFactory _pageFactory;
        private readonly IMasterDetailPageProvider _masterDetailPageProvider;

        public App(ILogger<App> logger, IDatabaseInitializationService databaseInitializationService, 
            IResourceInitializationService resourceInitializationService, 
            IPageFactory pageFactory, IMasterDetailPageProvider masterDetailPageProvider,
            ImageToImageSourceConverter converter)
        {
            _logger = logger;
            _databaseInitializationService = databaseInitializationService;
            _resourceInitializationService = resourceInitializationService;
            _pageFactory = pageFactory;
            _masterDetailPageProvider = masterDetailPageProvider;

            InitializeComponent();
            Material.Init(this, "Material.Configuration");
            Resources["Converter.ImageToImageSource"] = converter;

            // this is needed if the OnStart method is async
            MainPage = new Page();
        }

        // TODO: refactor the page creation logic out of here
        protected override async void OnStart()
        {
            base.OnStart();

            await _databaseInitializationService.InitializeDatabaseAsync();
            await _resourceInitializationService.InitializeResourcesAsync();

            var mainPage = _masterDetailPageProvider.GetMasterDetailPage();
            var masterPage = _pageFactory.CreatePage<MasterPage>();

            mainPage.Master = masterPage;
            await masterPage.InitializeViewmodelAsync(null);

            MainPage = mainPage;
            _logger.LogInformation("Application has successfully started!");
        }
    }
}
