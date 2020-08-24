using Xamarin.Forms;

namespace CooKit.Mobile.Pages.Recipes
{
    public partial class RecipeDetailPage
    {
        public RecipeDetailPage(DataTemplate stepTemplate)
        {
            InitializeComponent();
            StepCarousel.ItemTemplate = stepTemplate;
        }
    }
}