using CooKit.Views.Editor;
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
            Routing.RegisterRoute("editorMenu/editor", typeof(EditorMainView));

            Routing.RegisterRoute("editorMenu/editor/pictogramMenu", typeof(EditorPictogramMenuView));
        }
    }
}