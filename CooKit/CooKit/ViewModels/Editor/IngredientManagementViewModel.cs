using Autofac;
using CooKit.Models;
using CooKit.Services;
using CooKit.Views.Editor.Designers;

namespace CooKit.ViewModels.Editor
{
    public sealed class IngredientManagementViewModel : SharedManagementViewModel<IIngredient, IIngredientBuilder>
    {
        public IngredientManagementViewModel() : 
            base(App.Container.Resolve<IIngredientStore>(), typeof(IngredientDesignerView)) { }
    }
}
