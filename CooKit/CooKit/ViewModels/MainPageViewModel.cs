using CooKit.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CooKit.Models;
using CooKit.Services.Impl;
using CooKit.Services.Impl.ImageLoaders;
using Xamarin.Forms;
using CooKit.Services.Impl.SQLite;
using CooKit.Models.Impl;

namespace CooKit.ViewModels
{
    public class MainPageViewModel
    {
        private bool _isBusy;
        private readonly IRecipeStore _recipeStore;

        public ObservableCollection<IRecipe> Recipes { get; }

        public ICommand ThresholdReachedCommand { get; }

        public MainPageViewModel()
        {
            var imageStore = new ImageStoreImpl();
            imageStore.RegisterLoader("FileImageLoader", new FileImageLoader());

            //_recipeStore = new JsonRecipeStoreBuilder()
            //    .JsonStore.Set(new MockJsonStore())
            //    .ImageStore.Set(imageStore)
            //    .Build();

            _recipeStore = new SQLiteRecipeStoreBuilder()
                .ImageStore.Set(imageStore)
                .BuildAsync()
                .Result;

            _isBusy = false;
            Recipes = new ObservableCollection<IRecipe>();

            LoadRecipes();
            ThresholdReachedCommand = new Command(LoadRecipes);
        }

        private async void LoadRecipes()
        {
            if (_isBusy)
                return;

            _isBusy = true;

            for (var i = 0; i < 10; i++)
                Recipes.Add(await _recipeStore.GetNextRecipeAsync() ?? MockRecipe.Example);

            _isBusy = false;
        }
    }
}
