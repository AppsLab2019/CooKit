using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CooKit.ViewModels.Recipes
{
    public sealed class MainRecipeViewModel : ViewModel
    {
        public ICommand AllCommand => new Command(async () => await Open<AllRecipesViewModel>());
        public ICommand FavoriteCommand => new Command(async () => await Open<FavoriteRecipesViewModel>());

        private Task Open<T>() where T : IViewModel
        {
            return NavigationService.PushAsync<T>();
        }
    }
}
