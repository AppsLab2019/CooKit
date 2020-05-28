using System.Collections.ObjectModel;
using CooKit.Models.Editor.Ingredients;
using CooKit.Models.Editor.Steps;
using CooKit.Models.Pictograms;
using CooKit.Models.Recipes;

namespace CooKit.Models.Editor.Recipes
{
    public interface IEditorRecipe : IRecipe, IEditorModel
    {
        ObservableCollection<string> ObservableImages { get; set; }
        ObservableCollection<IPictogram> ObservablePictograms { get; set; }

        ObservableCollection<IEditorIngredient> ObservableIngredients { get; set; }
        ObservableCollection<IEditorStep> ObservableSteps { get; set; }
    }
}
