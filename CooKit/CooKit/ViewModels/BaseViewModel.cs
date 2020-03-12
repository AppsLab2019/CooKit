using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CooKit.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void HandlePropertyChange<T>(ref T backingField, T newValue, 
            [CallerMemberName] string propertyName = null)
        {
            if (Equals(backingField, newValue))
                return;

            backingField = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void HandlePropertyChangeCallback<T>(ref T backingField, T newValue,
            Action<T> callback, [CallerMemberName] string propertyName = null)
        {
            if (Equals(backingField, newValue))
                return;

            backingField = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            callback?.Invoke(newValue);
        }
    }
}
