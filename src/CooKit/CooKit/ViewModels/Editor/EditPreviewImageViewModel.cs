using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models.Recipes;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditPreviewImageViewModel : ViewModel
    {
        public override Task InitializeAsync(object parameter)
        {
            Recipe = parameter as IRecipe;
            
            if (Recipe is null)
                throw new ArgumentException(nameof(parameter));

            Image = Recipe.PreviewImage;
            EditableImage = Image;
            return Task.CompletedTask;
        }

        private async Task Save()
        {
            _recipe.PreviewImage = Image;
        }

        #region Commands

        public ICommand SaveCommand => new Command(async () => await Save());
        public ICommand SetCommand => new Command(() => Image = EditableImage);

        #endregion

        #region Properties

        public IRecipe Recipe
        {
            get => _recipe;
            private set => OnPropertyChange(ref _recipe, value);
        }

        public string Image
        {
            get => _image;
            private set => OnPropertyChange(ref _image, value);
        }

        public string EditableImage
        {
            get => _editableImage;
            set => OnPropertyChange(ref _editableImage, value);
        }

        #endregion

        #region Backing Fields

        private IRecipe _recipe;
        private string _image;
        private string _editableImage;

        #endregion
    }
}
