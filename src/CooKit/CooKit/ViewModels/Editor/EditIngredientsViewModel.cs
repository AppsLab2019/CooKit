using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Extensions;
using CooKit.Models.Editor.Ingredients;
using CooKit.Models.Editor.Recipes;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditIngredientsViewModel : ViewModel<IEditorRecipe>
    {
        private IEditorRecipe _recipe;

        public override Task InitializeAsync(IEditorRecipe recipe)
        {
            if (recipe is null)
                throw new ArgumentNullException(nameof(recipe));

            _recipe = recipe;
            
            // copy ingredients
            Ingredients = _recipe.ObservableIngredients.ToObservableCollection();
            return Task.CompletedTask;
        }

        public Task AddIngredient()
        {
            throw new NotImplementedException();
        }

        public Task EditIngredient(IEditorIngredient ingredient)
        {
            if (ingredient is null)
                return Task.CompletedTask;

            //return NavigationService.PushAsync<>();
            throw new NotImplementedException();
        }

        public void DeleteIngredient(IEditorIngredient ingredient)
        {
            Ingredients.Remove(ingredient);
        }

        public Task Save()
        {
            var copy = _ingredients.ToObservableCollection();
            _recipe.ObservableIngredients = copy;

            return NavigationService.PopAsync();
        }

        public ICommand AddIngredientCommand => new Command(async () => await AddIngredient());
        public ICommand EditIngredientCommand => new Command<IEditorIngredient>(async ingredient => await EditIngredient(ingredient));
        public ICommand DeleteIngredientCommand => new Command<IEditorIngredient>(DeleteIngredient);
        public ICommand SaveCommand => new Command(async () => await Save());

        public ObservableCollection<IEditorIngredient> Ingredients
        {
            get => _ingredients;
            set => OnPropertyChanged(ref _ingredients, value);
        }

        private ObservableCollection<IEditorIngredient> _ingredients;
    }
}
