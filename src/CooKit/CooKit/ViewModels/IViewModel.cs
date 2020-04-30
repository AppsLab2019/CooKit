using System.Threading.Tasks;

namespace CooKit.ViewModels
{
    public interface IViewModel
    {
        Task InitializeAsync(object parameter);
    }
}
