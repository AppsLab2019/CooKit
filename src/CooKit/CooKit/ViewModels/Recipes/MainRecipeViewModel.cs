using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Delegates;
using CooKit.Models.Recipes;
using CooKit.Services.Features;
using CooKit.Services.History;
using CooKit.ViewModels.Generic;
using Xamarin.Forms;

namespace CooKit.ViewModels.Recipes
{
    public sealed class MainRecipeViewModel : ViewModel
    {
        public ICommand AllCommand => new Command(async () => await Open<AllRecipesViewModel>());
        public ICommand FavoriteCommand => new Command(async () => await Open<FavoriteRecipesViewModel>());
        public ICommand HistoryCommand => new Command(async () => await Open<UnfinishedViewModel>());
        public ICommand OpenRecipeCommand => new Command<IRecipe>(async recipe => await Open<RecipeViewModel>(recipe));

        public IRecipe FeaturedRecipe { get; private set; }
        public IEnumerable<IRecipe> LastVisitedRecipes { get; private set; }

        private readonly IFeatureService _featureService;
        private readonly IHistoryService _historyService;

        public MainRecipeViewModel(IFeatureService featureService, IHistoryService historyService)
        {
            if (featureService is null)
                throw new ArgumentNullException(nameof(featureService));

            if (historyService is null)
                throw new ArgumentNullException(nameof(historyService));

            _featureService = featureService;
            _historyService = historyService;

            SubscribeToHistoryMessages();
        }

        private void SubscribeToHistoryMessages()
        {
            MessageHandlerSend<IHistoryService> handler = OnHistoryChanged;
            MessageBroker.Subscribe(this, handler, "Change");
        }

        public override async Task InitializeAsync(object parameter)
        {
            IsBusy = true;

            var featuredTask = RefreshFeaturedRecipe();
            var historyTask = RefreshHistory();

            await Task.WhenAll(featuredTask, historyTask);

            IsBusy = false;
        }

        private Task OnHistoryChanged(IHistoryService _, string __, object ___)
        {
            return RefreshHistory();
        }

        private async Task RefreshFeaturedRecipe()
        {
            FeaturedRecipe = await _featureService.GetFeaturedRecipe();
            RaisePropertyChanged(nameof(FeaturedRecipe));
        }

        private async Task RefreshHistory()
        {
            LastVisitedRecipes = await _historyService.GetLastVisitedRecipes();
            RaisePropertyChanged(nameof(LastVisitedRecipes));
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
