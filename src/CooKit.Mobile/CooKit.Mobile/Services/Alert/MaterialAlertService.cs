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
    }
}
