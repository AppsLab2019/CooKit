using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CooKit.Models;
using CooKit.Services;
using CooKit.Views.Editor;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class IngredientManagementViewModel : BaseViewModel, IDisposable
    {
        public ObservableCollection<IIngredient> Ingredients
        {
            get => _ingredients;
            private set => HandlePropertyChange(ref _ingredients, value);
        }
        private ObservableCollection<IIngredient> _ingredients;

        public IIngredient SelectedIngredient
        {
            get => _selectedIngredient;
            set => HandlePropertyChange(ref _selectedIngredient, value);
        }
        private IIngredient _selectedIngredient;

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }

        private readonly IIngredientStore _ingredientStore;

        public IngredientManagementViewModel()
        {
            _ingredientStore = App.GetIngredientStore();
            _ingredients = new ObservableCollection<IIngredient>(_ingredientStore.LoadedObjects);
            _selectedIngredient = null;

            AddCommand = new Command(HandleAdd);
            RemoveCommand = new Command(HandleRemove);

            _ingredientStore.PropertyChanged += HandleStoreContentChange;
        }

        private static async void HandleAdd() =>
            await Shell.Current.Navigation.PushAsync(new IngredientDesignerView());

        private async void HandleRemove()
        {
            if (SelectedIngredient is null)
                return;

            await _ingredientStore.RemoveAsync(_selectedIngredient.Id);
            SelectedIngredient = null;
        }

        private void HandleStoreContentChange(object sender, PropertyChangedEventArgs e)
        {
            if (sender != _ingredientStore)
                return;

            SelectedIngredient = null;
            Ingredients = new ObservableCollection<IIngredient>(_ingredientStore.LoadedObjects);
        }

        public void Dispose() =>
            _ingredientStore.PropertyChanged -= HandleStoreContentChange;
    }
}
