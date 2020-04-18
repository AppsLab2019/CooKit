using System;
using System.Collections.Generic;
using System.Windows.Input;
using CooKit.Models;
using CooKit.Services.Editor;
using CooKit.Services.Stores.Pictograms;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditorPictogramMenuViewModel : BaseViewModel
    {
        public ICommand InitCommand { get; }

        public IEnumerable<Pictogram> Pictograms { get; private set; }
        public IList<Pictogram> SelectedPictograms { get; private set; }

        private readonly IPictogramStore _store;
        private readonly IEditorService _editorService;
        public EditorPictogramMenuViewModel(IPictogramStore store, IEditorService editorService)
        {
            if (store is null)
                throw new ArgumentNullException(nameof(store));

            if (editorService is null)
                throw new ArgumentNullException(nameof(editorService));

            _store = store;
            _editorService = editorService;

            InitCommand = new Command(HandleInit);
        }

        private async void HandleInit()
        {
            var template = _editorService.GetTemplate();

            if (template is null)
                throw new Exception();

            Pictograms = await _store.GetAll();
            SelectedPictograms = template.Pictograms;

            RaiseAllPropertiesChanged();
        }
    }
}
