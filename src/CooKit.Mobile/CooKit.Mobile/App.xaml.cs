using CooKit.Mobile.Extensions;
using CooKit.Mobile.Factories.Page;
using CooKit.Mobile.Pages.Master;
using CooKit.Mobile.Providers.Page.MasterDetail;
using Microsoft.Extensions.Logging;

namespace CooKit.Mobile
{
    public partial class App
    {
        private readonly ILogger<App> _logger;
        private readonly IPageFactory _pageFactory;
        private readonly IMasterDetailPageProvider _masterDetailPageProvider;

        public App(ILogger<App> logger, IPageFactory pageFactory, IMasterDetailPageProvider masterDetailPageProvider)
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this, "Material.Configuration");

            _logger = logger;
            _pageFactory = pageFactory;
            _masterDetailPageProvider = masterDetailPageProvider;
        }

        // TODO: refactor the page creation logic out of here
        protected override async void OnStart()
        {
            base.OnStart();

            var mainPage = _masterDetailPageProvider.GetMasterDetailPage();
            var masterPage = _pageFactory.CreatePage<MasterPage>();

            mainPage.Master = masterPage;
            await masterPage.InitializeViewmodelAsync(null);

            MainPage = mainPage;
            _logger.LogInformation("Application has successfully started!");
        }
    }
}
