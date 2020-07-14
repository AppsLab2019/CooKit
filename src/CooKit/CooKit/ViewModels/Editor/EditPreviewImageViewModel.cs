using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models.Recipes;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditPreviewImageViewModel : ViewModel<IRecipe>
    {
        public override Task InitializeAsync(IRecipe recipe)
        {
            if (recipe is null)
                throw new ArgumentException(nameof(recipe));

            Recipe = recipe;
            Image = Recipe.PreviewImage;
            EditableImage = Image;

            return Task.CompletedTask;
        }

        private Task Save()
        {
            _recipe.PreviewImage = Image;
            return NavigationService.PopAsync();
        }

        #region Commands

        public ICommand SaveCommand => new Command(async () => await Save());
        public ICommand SetCommand => new Command(() => Image = EditableImage);
        public ICommand ClearCommand => new Command(() => EditableImage = string.Empty);

        #endregion

        #region Properties

        public IRecipe Recipe
        {
            get => _recipe;
            private set => OnPropertyChanged(ref _recipe, value);
        }

        public string Image
        {
            get => _image;
            private set => OnPropertyChanged(ref _image, value);
        }

        public string EditableImage
        {
            get => _editableImage;
            set => OnPropertyChanged(ref _editableImage, value);
        }

        #endregion

        #region Backing Fields

        private IRecipe _recipe;
        private string _image;
        private string _editableImage;

        #endregion
    }
}
