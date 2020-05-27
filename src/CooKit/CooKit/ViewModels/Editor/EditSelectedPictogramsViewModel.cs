using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CooKit.Models.Editor.Recipes;
using CooKit.Models.Pictograms;
using CooKit.Services.Stores.Pictograms;
using XF.Material.Forms.Models;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditSelectedPictogramsViewModel : ViewModel
    {
        private readonly IPictogramStore _store;

        public EditSelectedPictogramsViewModel(IPictogramStore store)
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

        public override async Task InitializeAsync(object parameter)
        {
            _recipe = parameter as IEditorRecipe;

            if (_recipe is null)
                throw new ArgumentException(nameof(parameter));

            IsBusy = true;

            AllPictograms = await _store.GetAll();
            SelectedPictograms = _recipe.ObservablePictograms;

            IsBusy = false;
        }

        public IList<MaterialMenuItem> PictogramChoices { get; }

        public IList<IPictogram> AllPictograms
        {
            get => _allPictograms;
            private set => OnPropertyChange(ref _allPictograms, value);
        }

        public ObservableCollection<IPictogram> SelectedPictograms
        {
            get => _selectedPictograms;
            private set => OnPropertyChange(ref _selectedPictograms, value);
        }

        private IEditorRecipe _recipe;
        private IList<IPictogram> _allPictograms;
        private ObservableCollection<IPictogram> _selectedPictograms;
    }
}
