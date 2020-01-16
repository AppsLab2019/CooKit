using CooKit.Models.Recipes;
using CooKit.ViewModels.Recipes;
using JetBrains.Annotations;

using Xamarin.Forms;

namespace CooKit.Views.Recipes
{
    public partial class RecipeIntroductionPage : ContentPage
    {
        public RecipeIntroductionPage([NotNull] IRecipe recipe)
        {
            InitializeComponent();

            BindingContext = new RecipeIntroductionViewModel(recipe);
        }
    }
}