using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CooKit.Models;
using CooKit.Views;
using Xamarin.Forms;

namespace CooKit.ViewModels
{
    public sealed class RecipeViewModel : INotifyPropertyChanged
    {
        public string Name { get; }
        public string Description { get; }
        public ImageSource MainImage { get; }
        public ObservableCollection<ImageSource> Pictograms { get; }

        public ObservableCollection<IIngredient> Ingredients { get; }
        public ObservableCollection<string> Steps { get; }

        public ICommand BackCommand { get; }
        public ICommand CookCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        public RecipeViewModel()
        {
            Name = "Recipe Name!";
            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas non sapien ante. Curabitur sit amet ullamcorper quam, euismod pharetra sem. Pellentesque turpis massa, egestas at dignissim et, sagittis a erat. Nulla quis blandit arcu, ac lobortis ligula. Nam lectus ante, vehicula et nisl auctor, pretium vehicula arcu.";
            MainImage = ImageSource.FromFile("food.jpg");
            Pictograms = new ObservableCollection<ImageSource>(new []{ ImageSource.FromFile("breakfast.png"), ImageSource.FromFile("breakfast.png") });

            Ingredients = new ObservableCollection<IIngredient>();
            Steps = new ObservableCollection<string>(new []{ "Boil an egg!", "Eat the egg!" });

            for (var i = 0; i < 10; i++)
                Ingredients.Add(new MockIngredient());

            BackCommand = new Command(() => Shell.Current.Navigation.PopAsync());
            CookCommand = new Command(() => Shell.Current.Navigation.PushAsync(new RecipeView(this)));
        }
    }
}
