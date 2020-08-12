using System.ComponentModel;
using System.Threading.Tasks;

namespace CooKit.Mobile.Viewmodels
{
    public abstract class ParameterlessBaseViewmodel : BaseViewmodel
    {
        protected abstract Task InitializeAsync();

        [EditorBrowsable(EditorBrowsableState.Never)]
        public sealed override Task InitializeAsync(object parameter)
        {
            return InitializeAsync();
        }
    }
}
