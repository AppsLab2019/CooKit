using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditImagesViewModel : ViewModel<ObservableCollection<string>>
    {
        public override Task InitializeAsync(ObservableCollection<string> parameter)
        {
            Images = parameter;
            return Task.CompletedTask;
        }

        private async Task AddImage()
        {
            var image = await AlertService.DisplayInput("Add image", "Enter source", 
                confirm: "Ok", cancel: "Cancel");

            if (string.IsNullOrEmpty(image))
                return;

            Images.Add(image);
        }

        private void RemoveImage(string image)
        {
            Images.Remove(image);
        }

        public ICommand AddCommand => new Command(async () => await AddImage());
        public ICommand RemoveCommand => new Command<string>(RemoveImage);

        public ObservableCollection<string> Images
        {
            get => _images;
            set => OnPropertyChange(ref _images, value);
        }

        private ObservableCollection<string> _images;
    }
}
