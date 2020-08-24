using System.Threading.Tasks;

namespace CooKit.Mobile.Services.Alert.Loading
{
    public interface ILoadingAlertService
    {
        Task<IRunningAlert> LoadingAlertAsync(string message);
    }
}
