using CooKit.ViewModels;
using Xamarin.Forms.Xaml;

namespace CooKit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeView
    {
        public RecipeView(RecipeViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }
    }
}