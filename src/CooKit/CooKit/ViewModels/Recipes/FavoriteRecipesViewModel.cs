using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Delegates;
using CooKit.Extensions;
using CooKit.Models.Recipes;
using CooKit.Services.Favorites;
using CooKit.Services.Messages;
using Xamarin.Forms;

namespace CooKit.ViewModels.Recipes
{
    public sealed class FavoriteRecipesViewModel : ViewModel
    {
        private readonly IFavoriteService _service;
        private readonly IMessageBroker _broker;

        public ObservableCollection<IRecipe> Recipes { get; private set; }

        public ICommand SelectCommand => new Command<IRecipe>(
            async recipe => await NavigationService.PushAsync<RecipeViewModel>(recipe));

        public FavoriteRecipesViewModel(IFavoriteService service, IMessageBroker broker)
        {
            if (service is null)
                throw new ArgumentNullException(nameof(service));

            if (broker is null)
                throw new ArgumentNullException(nameof(broker));

            _service = service;
            _broker = broker;

            SubscribeToFavoriteUpdates();
        }

        private void SubscribeToFavoriteUpdates()
        {
            MessageHandlerSendParam<IFavoriteService, IRecipe> handler = OnRecipeFavoriteStatusChanged;
            _broker.Subscribe(this, handler, "Toggled Favorite");
        }

        private Task OnRecipeFavoriteStatusChanged(IFavoriteService _, string __, IRecipe recipe)
        {
            if (recipe.IsFavorite)
                Recipes.Add(recipe);
            else
                Recipes.Remove(recipe);

            return Task.CompletedTask;
        }

        public override async Task InitializeAsync()
        {
            IsBusy = true;

            var recipes = await _service.GetFavoriteRecipes();
            Recipes = recipes.ToObservableCollection();
            RaisePropertyChanged(nameof(Recipes));

            IsBusy = false;
        }
    }
}
