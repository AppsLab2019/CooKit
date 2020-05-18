using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.ViewModels.Editor;
using CooKit.ViewModels.Generic;
using CooKit.ViewModels.Recipes;
using Xamarin.Forms;

namespace CooKit.ViewModels.Root
{
    public sealed class RootMasterViewModel : ViewModel
    {
        public IEnumerable<IRootDetailEntry> Entries { get; private set; }
        public ICommand SelectCommand => new Command<IRootDetailEntry>(async e => await SelectDetail(e));

        public override Task InitializeAsync(object parameter)
        {
            IsBusy = true;
            Entries = new[]
            {
                new RootDetailEntry
                {
                    Icon = "ic_action_menu.png",
                    Text = "Recipes",
                    ViewModelType = typeof(MainRecipeViewModel)
                },
                new RootDetailEntry
                {
                    Icon = "ic_action_edit.png",
                    Text = "Editor",
                    ViewModelType = typeof(EditorStartViewModel)
                },
                new RootDetailEntry
                {
                    Icon = null,
                    Text = "About",
                    ViewModelType = typeof(UnfinishedViewModel)
                }
            };

            RaisePropertyChanged(nameof(Entries));
            IsBusy = false;

            return Task.CompletedTask;
        }

        public async Task SelectDetail(IRootDetailEntry entry)
        {
            if (entry is null)
                return;

            await NavigationService.SetRootAsync(entry.ViewModelType);
        }
    }
}
