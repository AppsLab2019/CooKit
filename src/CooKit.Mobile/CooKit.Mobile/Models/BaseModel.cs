using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CooKit.Mobile.Models
{
    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(backingField, value))
                return;

            backingField = value;
            RaisePropertyChanged(propertyName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void RaiseAllPropertiesChanged()
        {
            RaisePropertyChanged(null);
        }
    }
}
