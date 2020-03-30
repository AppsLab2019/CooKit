using Autofac;
using CooKit.Models;
using CooKit.Services;

namespace CooKit.ViewModels.Editor.Designers
{
    public sealed class IngredientDesignerViewModel : BaseSimpleDesignerViewModel<IIngredient, IIngredientBuilder>
    {
        public IngredientDesignerViewModel() : base(App.Container.Resolve<IIngredientStore>()) { }
    }
}
