using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace CooKit.Mobile.Services.Alert.Loading
{
    public class MaterialLoadingRunningAlert : IRunningAlert
    {
        private readonly IMaterialModalPage _materialPage;

        public bool IsRunning { get; private set; }
        public string Message { get; private set; }

        public MaterialLoadingRunningAlert(IMaterialModalPage materialPage)
        {
            IsRunning = true;
            Message = materialPage.MessageText;
            _materialPage = materialPage;
        }

        public void SetMessage(string message)
        {
            Message = message;
            _materialPage.MessageText = message;
        }

        public async Task CloseAsync()
        {
            if (!IsRunning)
                return;

            await _materialPage.DismissAsync();
            IsRunning = false;
        }

        public ValueTask DisposeAsync()
        {
            if (!IsRunning)
                return default;

            var closingTask = CloseAsync();
            return new ValueTask(closingTask);
        }
    }
}
