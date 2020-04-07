using System.Threading.Tasks;

namespace CooKit.Services.Alerts
{
    public interface IAlertService
    {
        Task DisplayAlert(string title, string message, string cancel);
    }
}
