using System.Collections.ObjectModel;
using CooKit.Models.Ingredients;
using CooKit.Models.Pictograms;
using CooKit.Models.Recipes;
using CooKit.Models.Steps;

namespace CooKit.Models.Editor.Recipe
{
    public interface IEditorRecipe : IRecipe, IEditorModel
    {
        ObservableCollection<string> ObservableImages { get; set; }
        ObservableCollection<IIngredient> ObservableIngredients { get; set; }
        ObservableCollection<IPictogram> ObservablePictograms { get; set; }
        ObservableCollection<IStep> ObservableSteps { get; set; }
    }
}
