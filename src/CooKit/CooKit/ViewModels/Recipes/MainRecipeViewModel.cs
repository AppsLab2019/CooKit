using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models.Recipes;
using CooKit.Services.Features;
using CooKit.ViewModels.Generic;
using Xamarin.Forms;

namespace CooKit.ViewModels.Recipes
{
    public sealed class MainRecipeViewModel : ViewModel
    {
        public ICommand AllCommand => new Command(async () => await Open<AllRecipesViewModel>());
        public ICommand FavoriteCommand => new Command(async () => await Open<FavoriteRecipesViewModel>());
        public ICommand HistoryCommand => new Command(async () => await Open<UnfinishedViewModel>());

        public IRecipe FeaturedRecipe { get; private set; }
        public ICommand OpenFeaturedRecipeCommand => new Command(async () => await Open<RecipeViewModel>(FeaturedRecipe));

        private readonly IFeatureService _featureService;

        public MainRecipeViewModel(IFeatureService featureService)
        {
            if (featureService is null)
                throw new ArgumentNullException(nameof(featureService));

            _featureService = featureService;
        }

        public override async Task InitializeAsync(object parameter)
        {
            IsBusy = true;

            FeaturedRecipe = await _featureService.GetFeaturedRecipe();
            RaisePropertyChanged(nameof(FeaturedRecipe));

            IsBusy = false;
        }

        private Task Open<T>() where T : IViewModel
        {
            return NavigationService.PushAsync<T>();
        }

        private Task Open<T>(object parameter) where T : IViewModel
        {
            return NavigationService.PushAsync<T>(parameter);
        }
    }
}
