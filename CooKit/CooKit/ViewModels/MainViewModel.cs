﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using CooKit.Models;
using CooKit.Views;
using Xamarin.Forms;

namespace CooKit.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ReadOnlyCollection<IRecipe> Recipes { get; }
        public ICommand OpenRecipeCommand { get; }

        public MainViewModel()
        {
            Recipes = App.GetRecipeStore().LoadedObjects;
            OpenRecipeCommand = new Command<IRecipe>(HandleOpenRecipe);
        }

        private async void HandleOpenRecipe(IRecipe recipe)
        {
            if (recipe is null)
                return;

            var page = new RecipeView { BindingContext = new RecipeViewModel(recipe) };

            await PushAsync(page);
        }
    }
}
