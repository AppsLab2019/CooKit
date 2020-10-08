using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Mobile.Models.Root;
using CooKit.Mobile.Registries.RootEntry;
using CooKit.Mobile.Services.Root;
using Xamarin.Forms;

namespace CooKit.Mobile.Viewmodels
{
    // TODO: add logging
    public class MasterViewmodel : ParameterlessBaseViewmodel
    {
        private readonly IRootService _rootService;
        private readonly IRootEntryRegistry _registry;

        public ICommand SelectCommand => new Command<IRootEntry>(async entry => await SetCurrentEntryAsync(entry));

        public MasterViewmodel(IRootService rootService, IRootEntryRegistry registry)
        {
            _rootService = rootService;
            _registry = registry;
        }

        public IList<IRootEntry> Entries
        {
            get => _entries;
            set => OnPropertyChanged(ref _entries, value);
        }

        public IRootEntry CurrentEntry
        {
            get => _currentEntry;
            set => OnPropertyChanged(ref _currentEntry, value);
        }

        private IList<IRootEntry> _entries;
        private IRootEntry _currentEntry;

        protected override Task InitializeAsync()
        {
            Entries = _registry.GetEntries();

            var defaultEntry = Entries.First();
            return SetCurrentEntryAsync(defaultEntry);
        }

        private async Task SetCurrentEntryAsync(IRootEntry entry)
        {
            if (entry == null)
                return;

            if (entry == CurrentEntry)
                return;

            await _rootService.SetRootAsync(entry.ViewmodelType);
            CurrentEntry = entry;
        }
    }
}
