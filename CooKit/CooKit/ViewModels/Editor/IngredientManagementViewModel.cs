using CooKit.Models;
using CooKit.Views.Editor.Designers;

namespace CooKit.ViewModels.Editor
{
    public sealed class IngredientManagementViewModel : SharedManagementViewModel<IIngredient, IIngredientBuilder>
    {
        public IngredientManagementViewModel() : 
            base(App.GetIngredientStore(), typeof(IngredientDesignerView)) { }
    }
}
