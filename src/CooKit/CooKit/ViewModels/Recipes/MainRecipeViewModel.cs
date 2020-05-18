using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.ViewModels.Generic;
using Xamarin.Forms;

namespace CooKit.ViewModels.Recipes
{
    public sealed class MainRecipeViewModel : ViewModel
    {
        public ICommand AllCommand => new Command(async () => await Open<AllRecipesViewModel>());
        public ICommand FavoriteCommand => new Command(async () => await Open<FavoriteRecipesViewModel>());
        public ICommand HistoryCommand => new Command(async () => await Open<UnfinishedViewModel>());

        private Task Open<T>() where T : IViewModel
        {
            return NavigationService.PushAsync<T>();
        }
    }
}
