using System.Threading.Tasks;

namespace CooKit.Mobile.Services.Alert
{
    public interface IAlertService
    {
        Task AlertAsync(string title, string message, string close);

        Task<string> InputAsync(string title, string message, string confirm, string cancel);
        Task<string> InputAsync(string title, string message, string placeholder, string confirm, string cancel);
        Task<string> InputAsync(string title, string message, string input, string placeholder, 
            string confirm, string cancel);
    }
}
