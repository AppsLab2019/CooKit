using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace CooKit.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Alert Functions

        protected Task DisplayAlert(string title, string message, string cancel) =>
            MaterialDialog.Instance.AlertAsync(message, title, cancel);

        protected Task<bool> DisplayAlert(string title, string message, string accept, string cancel) =>
            MaterialDialog.Instance.ConfirmAsync(message, title, accept, cancel)
                .ContinueWith(task => task.Result ?? false);

        #endregion

        #region Alert Sheet Function

        protected Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons) =>
            Shell.Current.DisplayActionSheet(title, cancel, destruction, buttons);

        #endregion

        #region Loading Dialog Function

        protected Task<IMaterialModalPage> DisplayLoadingDialog(string message = null) =>
            MaterialDialog.Instance.LoadingDialogAsync(message);

        #endregion

        #region Navigation

        protected Task PushAsync(Page page) =>
            Shell.Current.Navigation.PushAsync(page);

        protected Task<Page> PopAsync() =>
            Shell.Current.Navigation.PopAsync();

        #endregion

        #region Permissions

        protected Task<bool> RequestPermission<T>(string errorMessage) where T : Permissions.BasePermission, new() =>
            RequestPermission<T>(() => DisplayAlert("Error", errorMessage, "Cancel"));

        protected async Task<bool> RequestPermission<T>(Func<Task> errorCallbackFunc) where T : Permissions.BasePermission, new()
        {
            var status = await Permissions.CheckStatusAsync<T>();

            if (status != PermissionStatus.Granted)
                status = await Permissions.RequestAsync<T>();

            if (status == PermissionStatus.Granted)
                return true;

            await errorCallbackFunc();
            return false;
        }

        #endregion

        #region Property Change Handlers

        protected void HandlePropertyChange<T>(ref T backingField, T newValue, 
            [CallerMemberName] string propertyName = null)
        {
            if (Equals(backingField, newValue))
                return;

            backingField = newValue;
            RaisePropertyChanged(propertyName);
        }

        protected void HandlePropertyChangeCallback<T>(ref T backingField, T newValue,
            Action<T> callback, [CallerMemberName] string propertyName = null)
        {
            if (Equals(backingField, newValue))
                return;

            backingField = newValue;
            RaisePropertyChanged(propertyName);
            callback?.Invoke(newValue);
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void RaisePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}
