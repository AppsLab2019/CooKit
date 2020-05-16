using System;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace CooKit.Services.Snackbar
{
    public sealed class SnackbarService : ISnackbarService
    {
        public Task SnackbarAsync(string message, int duration)
        {
            return MaterialDialog.Instance.SnackbarAsync(message, duration);
        }

        public Task SnackbarAsync(string message, TimeSpan duration)
        {
            return SnackbarAsync(message, (int) duration.TotalMilliseconds);
        }

        public Task<bool> SnackbarAsync(string message, string buttonText, int duration)
        {
            return MaterialDialog.Instance.SnackbarAsync(message, buttonText, duration);
        }

        public Task<bool> SnackbarAsync(string message, string buttonText, TimeSpan duration)
        {
            return SnackbarAsync(message, buttonText, (int) duration.TotalMilliseconds);
        }
    }
}
