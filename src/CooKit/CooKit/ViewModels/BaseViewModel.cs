using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CooKit.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public bool IsBusy
        {
            get => _isBusy;
            set => OnPropertyChange(ref _isBusy, value);
        }

        private bool _isBusy;

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange<T>(ref T field, T value, 
            [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value))
                return;

            field = value;
            RaisePropertyChanged(propertyName);
        }

        protected void RaiseAllPropertiesChanged() =>
            RaisePropertyChanged(null);

        protected void RaisePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}
