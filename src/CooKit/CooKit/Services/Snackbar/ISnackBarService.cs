using System;
using System.Threading.Tasks;

namespace CooKit.Services.Snackbar
{
    public interface ISnackbarService
    {
        Task SnackbarAsync(string message, int duration);
        Task SnackbarAsync(string message, TimeSpan duration);

        Task<bool> SnackbarAsync(string message, string buttonText, int duration);
        Task<bool> SnackbarAsync(string message, string buttonText, TimeSpan duration);
    }
}
