using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CooKit.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Alert Functions

        protected Task DisplayAlert(string title, string message, string cancel) =>
            Shell.Current.DisplayAlert(title, message, cancel);

        protected Task<bool> DisplayAlert(string title, string message, string accept, string cancel) =>
            Shell.Current.DisplayAlert(title, message, accept, cancel);

        #endregion

        #region Alert Sheet Function

        public Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons) =>
            Shell.Current.DisplayActionSheet(title, cancel, destruction, buttons);

        #endregion

        #region Navigation

        protected Task PushAsync(Page page) =>
            Shell.Current.Navigation.PushAsync(page);

        protected Task<Page> PopAsync() =>
            Shell.Current.Navigation.PopAsync();

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void RaisePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
