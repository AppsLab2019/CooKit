using System.Collections.ObjectModel;
using System.Windows.Input;
using CooKit.Models;
using CooKit.Views;
using Xamarin.Forms;

namespace CooKit.ViewModels
{
    public sealed class RecipeViewModel
    {
        public string Name => 
            _recipe.Name;
        public string Description => 
            _recipe.Description;
        public ImageSource MainImage => 
            _recipe.MainImage;

        public ObservableCollection<IPictogram> Pictograms { get; }
        public ObservableCollection<IIngredient> Ingredients { get; }
        public ObservableCollection<string> Steps { get; }

        public ICommand BackCommand { get; }
        public ICommand CookCommand { get; }

        private readonly IRecipe _recipe;

        public RecipeViewModel(IRecipe recipe)
        {
            Pictograms = new ObservableCollection<IPictogram>(recipe.Pictograms);

            Ingredients = new ObservableCollection<IIngredient>(recipe.Ingredients);
            Steps = new ObservableCollection<string>(recipe.Steps);

            BackCommand = new Command(() => Shell.Current.Navigation.PopAsync());
            CookCommand = new Command(() => Shell.Current.Navigation.PushAsync(new RecipeView(this)));

            _recipe = recipe;
        }
    }
}
