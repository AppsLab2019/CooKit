using Autofac;
using CooKit.Models;
using CooKit.Services;
using CooKit.Views.Editor.Designers;

namespace CooKit.ViewModels.Editor
{
    public sealed class RecipeManagementViewModel : SharedManagementViewModel<IRecipe, IRecipeBuilder>
    {
        public RecipeManagementViewModel() 
            : base(App.Container.Resolve<IRecipeStore>(), typeof(RecipeDesignerView)) { }
    }
}
