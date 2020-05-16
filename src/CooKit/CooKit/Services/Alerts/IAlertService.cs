using System;
using System.Threading.Tasks;

namespace CooKit.Services.Alerts
{
    public interface IAlertService
    {
        Task DisplayAlert(string title, string message, string cancel);

        Task<bool> DisplayAlert(string title, string message, string confirm, string cancel);

        Task<string> DisplayInput(string title = null, string message = null, string inputText = null,
            string inputPlaceholder = null, string confirm = null, string cancel = null);

        // TODO: replace output with custom class
        Task<IDisposable> DisplayLoading(string message);
    }
}
