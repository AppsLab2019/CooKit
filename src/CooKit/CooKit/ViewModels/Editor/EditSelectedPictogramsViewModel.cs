﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CooKit.Models.Editor.Recipes;
using CooKit.Models.Pictograms;
using CooKit.Services.Stores;
using XF.Material.Forms.Models;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditSelectedPictogramsViewModel : ViewModel<IEditorRecipe>
    {
        private readonly IStore<IPictogram> _store;

        public EditSelectedPictogramsViewModel(IStore<IPictogram> store)
        {
            if (store is null)
                throw new ArgumentNullException(nameof(store));

            _store = store;

            PictogramChoices = new List<MaterialMenuItem>
            {
                new MaterialMenuItem { Text = "Edit" },
                new MaterialMenuItem { Text = "Delete" }
            };
        }

        public override async Task InitializeAsync(IEditorRecipe recipe)
        {
            if (recipe is null)
                throw new ArgumentException(nameof(recipe));

            IsBusy = true;

            _recipe = recipe;
            AllPictograms = await _store.GetAll();
            SelectedPictograms = _recipe.ObservablePictograms;

            IsBusy = false;
        }

        public IList<MaterialMenuItem> PictogramChoices { get; }

        public IList<IPictogram> AllPictograms
        {
            get => _allPictograms;
            private set => OnPropertyChanged(ref _allPictograms, value);
        }

        public ObservableCollection<IPictogram> SelectedPictograms
        {
            get => _selectedPictograms;
            private set => OnPropertyChanged(ref _selectedPictograms, value);
        }

        private IEditorRecipe _recipe;
        private IList<IPictogram> _allPictograms;
        private ObservableCollection<IPictogram> _selectedPictograms;
    }
}
