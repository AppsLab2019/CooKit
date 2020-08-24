using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace CooKit.Mobile.Services.Alert.Loading
{
    public class MaterialLoadingAlertService : ILoadingAlertService
    {
        public async Task<IRunningAlert> LoadingAlertAsync(string message)
        {
            var page = await MaterialDialog.Instance.LoadingDialogAsync(message);
            return new MaterialLoadingRunningAlert(page);
        }
    }
}
