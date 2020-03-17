using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CooKit.Models;
using CooKit.Services;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public class SharedManagementViewModel<TStorable, TStorableBuilder> : BaseViewModel where TStorable : IStorable
    {
        public ReadOnlyObservableCollection<TStorable> StoredObjects { get; }

        public TStorable SelectedObject
        {
            get => _selectedObject;
            set => HandlePropertyChange(ref _selectedObject, value);
        }
        private TStorable _selectedObject;

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }

        private readonly IStoreBase<TStorable, TStorableBuilder> _store;
        private readonly Type _designerPageType;

        public SharedManagementViewModel(IStoreBase<TStorable, TStorableBuilder> store, Type designerPageType)
        {
            _store = store;
            _designerPageType = designerPageType;

            _selectedObject = default;
            StoredObjects = store.LoadedObjects;
            
            AddCommand = new Command(HandleAdd);
            RemoveCommand = new Command(HandleRemove);
        }

        private async void HandleAdd()
        {
            var page = (Page) Activator.CreateInstance(_designerPageType);
            await PushAsync(page);
        }

        private async void HandleRemove()
        {
            if (SelectedObject is null)
                return;

            using (await DisplayLoadingDialog("Removing..."))
                await _store.RemoveAsync(SelectedObject.Id);

            SelectedObject = default;
        }
    }
}
