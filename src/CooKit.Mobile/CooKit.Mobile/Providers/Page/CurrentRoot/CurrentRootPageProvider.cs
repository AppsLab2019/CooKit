using CooKit.Mobile.Providers.Page.MasterDetail;
using Xamarin.Forms;

namespace CooKit.Mobile.Providers.Page.CurrentRoot
{
    public class CurrentRootPageProvider : ICurrentRootPageProvider
    {
        private readonly IMasterDetailPageProvider _pageProvider;

        public CurrentRootPageProvider(IMasterDetailPageProvider pageProvider)
        {
            _pageProvider = pageProvider;
        }

        public NavigationPage GetCurrentRootPage()
        {
            var masterDetailPage = _pageProvider.GetMasterDetailPage();
            var rootPage = masterDetailPage?.Detail;
            return rootPage as NavigationPage;
        }
    }
}
