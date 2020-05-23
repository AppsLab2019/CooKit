using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models.Recipes;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditMainInfoViewModel : ViewModel
    {
        private IRecipe _recipe;

        public override Task InitializeAsync(object parameter)
        {
            _recipe = parameter as IRecipe;
            
            if (_recipe is null)
                throw new ArgumentException(nameof(parameter));

            Name = _recipe.Name;
            Description = _recipe.Description;

            EditableName = Name;
            EditableDescription = Description;

            return Task.CompletedTask;
        }

        private Task Save()
        {
            return Task.CompletedTask;
        }

        private void SetChanges()
        {
            Name = EditableName;
            Description = EditableDescription;
        }

        public ICommand SaveCommand => new Command(async () => await Save());
        public ICommand SetCommand => new Command(SetChanges);

        public string Name
        {
            get => _name;
            private set => OnPropertyChange(ref _name, value);
        }

        public string Description
        {
            get => _description;
            private set => OnPropertyChange(ref _description, value);
        }

        public string EditableName
        {
            get => _editableName;
            set => OnPropertyChange(ref _editableName, value);
        }

        public string EditableDescription
        {
            get => _editableDescription;
            set => OnPropertyChange(ref _editableDescription, value);
        }

        private string _name;
        private string _description;
        private string _editableName;
        private string _editableDescription;
    }
}
