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

        protected Task DisplayAlert(string title, string message, string cancel) =>
            Shell.Current.DisplayAlert(title, message, cancel);

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void RaisePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
