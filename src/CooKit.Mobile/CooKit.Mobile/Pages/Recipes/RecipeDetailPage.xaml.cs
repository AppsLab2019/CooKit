using CooKit.Mobile.Selectors.Steps;

namespace CooKit.Mobile.Pages.Recipes
{
    public partial class RecipeDetailPage
    {
        public RecipeDetailPage(RecipeDetailStepTemplateSelector stepTemplateSelector)
        {
            InitializeComponent();
            StepCarousel.ItemTemplate = stepTemplateSelector;
        }
    }
}