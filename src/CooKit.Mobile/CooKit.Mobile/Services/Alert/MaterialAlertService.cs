using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace CooKit.Mobile.Services.Alert
{
    public class MaterialAlertService : IAlertService
    {
        public Task AlertAsync(string title, string message, string close)
        {
            return MaterialDialog.Instance.AlertAsync(message, title, close);
        }

        public Task<string> InputAsync(string title, string message, string confirm, string cancel)
        {
            return MaterialDialog.Instance.InputAsync(title, message, confirmingText: confirm, dismissiveText: cancel);
        }

        public Task<string> InputAsync(string title, string message, string placeholder, string confirm, string cancel)
        {
            return MaterialDialog.Instance.InputAsync(title, message, inputPlaceholder: placeholder,
                confirmingText: confirm, dismissiveText: cancel);
        }

        public Task<string> InputAsync(string title, string message, string input, string placeholder, string confirm, string cancel)
        {
            return MaterialDialog.Instance.InputAsync(title, message, input, placeholder, confirm, cancel);
        }
    }
}
