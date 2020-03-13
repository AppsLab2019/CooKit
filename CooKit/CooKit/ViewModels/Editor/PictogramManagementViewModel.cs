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
    public sealed class PictogramManagementViewModel : BaseViewModel, IDisposable
    {
        public ObservableCollection<IPictogram> Pictograms
        {
            get => _pictograms;
            private set => HandlePropertyChange(ref _pictograms, value);
        }
        private ObservableCollection<IPictogram> _pictograms;

        public IPictogram SelectedPictogram
        {
            get => _selectedPictogram;
            set => HandlePropertyChange(ref _selectedPictogram, value);
        }
        private IPictogram _selectedPictogram;

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }

        private readonly IPictogramStore _pictogramStore;

        public PictogramManagementViewModel()
        {
            _pictogramStore = App.GetPictogramStore();
            _pictograms = new ObservableCollection<IPictogram>(_pictogramStore.LoadedObjects);
            _selectedPictogram = null;

            AddCommand = new Command(HandleAdd);
            RemoveCommand = new Command(HandleRemove);

            _pictogramStore.PropertyChanged += HandleStoreContentChange;
        }

        private static async void HandleAdd() =>
            await Shell.Current.Navigation.PushAsync(new PictogramDesignerView());

        private async void HandleRemove()
        {
            if (SelectedPictogram is null)
                return;

            await _pictogramStore.RemoveAsync(_selectedPictogram.Id);
            SelectedPictogram = null;
        }

        private void HandleStoreContentChange(object sender, PropertyChangedEventArgs e)
        {
            if (sender != _pictogramStore)
                return;

            SelectedPictogram = null;
            Pictograms = new ObservableCollection<IPictogram>(_pictogramStore.LoadedObjects);
        }

        public void Dispose() =>
            _pictogramStore.PropertyChanged -= HandleStoreContentChange;
    }
}
