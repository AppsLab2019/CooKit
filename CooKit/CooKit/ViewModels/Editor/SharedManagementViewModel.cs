using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CooKit.Models;
using CooKit.Services;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public class SharedManagementViewModel<TStorable, TStorableBuilder> 
        : BaseViewModel where TStorable : IStorable
    {
        public ObservableCollection<TStorable> StoredObjects
        {
            get => _storedObjects;
            set => HandlePropertyChange(ref _storedObjects, value);
        }
        private ObservableCollection<TStorable> _storedObjects;

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
            _storedObjects = new ObservableCollection<TStorable>(store.LoadedObjects);
            
            AddCommand = new Command(HandleAdd);
            RemoveCommand = new Command(HandleRemove);

            _store.PropertyChanged += HandleStoreContentChange;
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

            await _store.RemoveAsync(SelectedObject.Id);
            SelectedObject = default;
        }

        private void HandleStoreContentChange(object sender, PropertyChangedEventArgs e)
        {
            if (sender != _store)
                return;

            SelectedObject = default;
            StoredObjects = new ObservableCollection<TStorable>(_store.LoadedObjects);
        }
    }
}
