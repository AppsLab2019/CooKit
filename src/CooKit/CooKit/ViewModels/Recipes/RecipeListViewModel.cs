﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models;
using CooKit.Services.Repositories.Recipes;
using Xamarin.Forms;

namespace CooKit.ViewModels.Recipes
{
    public sealed class RecipeListViewModel : IRecipeListViewModel
    {
        public IEnumerable<Recipe> Recipes { get; private set; }
        public ICommand RefreshCommand { get; }
        public ICommand SelectCommand { get; }

        private readonly IRecipeRepository _repository;

        public RecipeListViewModel(IRecipeRepository repository)
        {
            if (repository is null)
                throw new ArgumentNullException(nameof(repository));

            RefreshCommand = new Command(async () => await RefreshRecipes());
            SelectCommand = new Command<Recipe>(async recipe => await SelectRecipe(recipe));

            _repository = repository;
        }

        private async Task RefreshRecipes()
        {
            Recipes = await _repository.GetAllEntries();
        }

        private Task SelectRecipe(Recipe recipe)
        {
            throw new NotImplementedException();
        }
    }
}
