using System;
using System.Threading.Tasks;

namespace CooKit.Mobile.Services.Alert
{
    public interface IRunningAlert : IAsyncDisposable
    {
        bool IsRunning { get; }
        string Message { get; }
        void SetMessage(string message);
        Task CloseAsync();
    }
}
