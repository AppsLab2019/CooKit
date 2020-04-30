using System.Collections.Generic;
using System.Linq;
using CooKit.ViewModels.Editor;
using CooKit.ViewModels.Recipes;

namespace CooKit.ViewModels.Root
{
    public sealed class RootMasterViewModel : ViewModel
    {
        public IEnumerable<IRootDetailEntry> Entries { get; }
        public IRootDetailEntry SelectedEntry { get; set; }

        public RootMasterViewModel()
        {
            Entries = new[]
            {
                new RootDetailEntry
                {
                    Icon = null,
                    Text = "Recipes",
                    ViewModelType = typeof(RecipeListViewModel)
                },

                new RootDetailEntry
                {
                    Icon = null,
                    Text = "Editor",
                    ViewModelType = typeof(EditorStartViewModel)
                }
            };

            SelectedEntry = Entries.First();
        }
    }
}
