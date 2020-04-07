using System.Threading.Tasks;
using Xamarin.Forms;

namespace CooKit.Services.Alerts
{
    public sealed class AlertService : IAlertService
    {
        public Task DisplayAlert(string title, string message, string cancel)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}
