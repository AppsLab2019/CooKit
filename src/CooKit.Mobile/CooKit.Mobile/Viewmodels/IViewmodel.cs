using System.Threading.Tasks;

namespace CooKit.Mobile.Viewmodels
{
    public interface IViewmodel
    {
        Task InitializeAsync(object parameter);
    }
}
