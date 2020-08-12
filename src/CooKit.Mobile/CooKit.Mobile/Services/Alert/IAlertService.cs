using System.Threading.Tasks;

namespace CooKit.Mobile.Services.Alert
{
    public interface IAlertService
    {
        Task AlertAsync(string title, string message, string close);
    }
}
