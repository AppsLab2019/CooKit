using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models.Recipes;
using CooKit.Services.Editor;
using CooKit.Services.Stores.Recipes;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditorStartViewModel : ViewModel
    {
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        public IRecipe SelectedRecipe { get; set; }
        public IEnumerable<IRecipe> Recipes { get; private set; }

        private readonly IRecipeStore _store;
        private readonly IEditorService _editorService;

        public EditorStartViewModel(IRecipeStore store, IEditorService editorService)
        {
            if (store is null)
                throw new ArgumentNullException(nameof(store));

            if (editorService is null)
                throw new ArgumentNullException(nameof(editorService));

            _store = store;
            _editorService = editorService;

            AddCommand = new Command(HandleAdd);
            EditCommand = new Command(HandleEdit);
            DeleteCommand = new Command(HandleDelete);
            RefreshCommand = new Command(HandleRefresh);
        }

        private async void HandleRefresh()
        {
            Recipes = await _store.GetAll();
            SelectedRecipe = null;

            RaiseAllPropertiesChanged();
        }

        private void HandleAdd()
        {
            _editorService.CreateNewRecipe();
            GoToEditor();
        }

        private void HandleEdit()
        {
            if (SelectedRecipe is null)
                return;

            _editorService.SetWorkingRecipe(SelectedRecipe);
            GoToEditor();
        }

        private void HandleDelete()
        {
            if (SelectedRecipe is null)
                return;

            throw new NotImplementedException();
        }

        private Task GoToEditor()
        {
            return Shell.Current.GoToAsync("editorMenu/editor");
        }
    }
}
