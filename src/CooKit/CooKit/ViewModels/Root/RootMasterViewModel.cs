using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models.RootDetails;
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

        public override Task InitializeAsync()
        {
            IsBusy = true;
            Entries = new[]
            {
                new RootDetailEntry
                {
                    Icon = "ic_action_home.png",
                    Text = "Home",
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
                    Icon = "ic_action_group.png",
                    Text = "About",
                    ViewModelType = typeof(UnfinishedViewModel)
                }
            };

            RaisePropertyChanged(nameof(Entries));
            IsBusy = false;

            return Task.CompletedTask;
        }

        public Task SelectDetail(IRootDetailEntry entry)
        {
            if (entry is null)
                return Task.CompletedTask;

            return NavigationService.SetRootAsync(entry.ViewModelType);
        }
    }
}
