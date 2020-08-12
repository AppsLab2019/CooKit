namespace CooKit.Mobile.Providers.Page.Main
{
    public interface IMainPageProvider
    {
        Xamarin.Forms.Page GetMainPage();
        void SetMainPage(Xamarin.Forms.Page page);
    }
}
