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

            return Task.CompletedTask;
        }

        private Task Save()
        {
            _recipe.Name = Name;
            _recipe.Description = Description;

            return NavigationService.PopAsync();
        }

        public ICommand SaveCommand => new Command(async () => await Save());

        public string Name
        {
            get => _name;
            set => OnPropertyChange(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => OnPropertyChange(ref _description, value);
        }

        private string _name;
        private string _description;
    }
}
