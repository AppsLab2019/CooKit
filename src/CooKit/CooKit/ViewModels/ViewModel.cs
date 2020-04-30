using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CooKit.ViewModels
{
    public class ViewModel : IViewModel, INotifyPropertyChanged
    {
        public virtual Task InitializeAsync(object parameter)
        {
            return Task.CompletedTask;
        }

        #region IsBusy

        public bool IsBusy
        {
            get => _isBusy;
            set => OnPropertyChange(ref _isBusy, value);
        }

        private bool _isBusy;

        #endregion

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
