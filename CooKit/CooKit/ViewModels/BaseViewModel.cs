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
    }
}
