using Xamarin.Forms.Xaml;

namespace CooKit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeView
    {
        public RecipeView()
        {
            InitializeComponent();
        }
    }

    public sealed class TemporaryRecipeStep
    {
        public string Value { get; set; }
    }
}