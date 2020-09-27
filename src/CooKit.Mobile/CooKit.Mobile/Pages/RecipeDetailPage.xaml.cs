using Xamarin.Forms;

namespace CooKit.Mobile.Pages
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