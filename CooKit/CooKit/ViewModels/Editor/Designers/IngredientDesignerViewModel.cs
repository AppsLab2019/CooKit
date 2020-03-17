using CooKit.Models;

namespace CooKit.ViewModels.Editor.Designers
{
    public sealed class IngredientDesignerViewModel : BaseSimpleDesignerViewModel<IIngredient, IIngredientBuilder>
    {
        public IngredientDesignerViewModel() : base(App.GetIngredientStore()) { }
    }
}
