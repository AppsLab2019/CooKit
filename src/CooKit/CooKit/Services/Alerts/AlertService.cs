using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace CooKit.Services.Alerts
{
    public sealed class AlertService : IAlertService
    {
        public Task DisplayAlert(string title, string message, string cancel)
        {
            return MaterialDialog.Instance.AlertAsync(message, title, cancel);
        }

        public async Task<bool> DisplayAlert(string title, string message, string confirm, string cancel)
        {
            var output = await MaterialDialog.Instance.ConfirmAsync
                (message, title, confirm, cancel);
            
            return output ?? false;
        }

        public Task<string> DisplayInput(string title = null, string message = null, string inputText = null, 
            string inputPlaceholder = null, string confirm = null, string cancel = null)
        {
            return MaterialDialog.Instance.InputAsync(title, message, inputText, 
                inputPlaceholder, confirm, cancel);
        }

        public Task<int> DisplaySelectAction(string title, IList<string> actions)
        {
            return MaterialDialog.Instance.SelectActionAsync(title, actions);
        }

        public async Task<IDisposable> DisplayLoading(string message)
        {
            return await MaterialDialog.Instance.LoadingDialogAsync(message);
        }
    }
}