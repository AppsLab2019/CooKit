using CooKit.Views.Recipes;
using Xamarin.Forms;

namespace CooKit
{
    public partial class AppShell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("recipes/all/recipe", typeof(RecipeView));
        }
    }
}