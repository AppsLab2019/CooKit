using Xamarin.Forms;

namespace CooKit.Mobile.Providers.Page.Main
{
    public class MainPageProvider : IMainPageProvider
    {
        private readonly Application _application;

        public MainPageProvider(Application application)
        {
            _application = application;
        }

        public Xamarin.Forms.Page GetMainPage()
        {
            return _application.MainPage;
        }

        public void SetMainPage(Xamarin.Forms.Page page)
        {
            _application.MainPage = page;
        }
    }
}
