using System.Collections.ObjectModel;
using CooKit.Models.Editor.Ingredients;
using CooKit.Models.Pictograms;
using CooKit.Models.Recipes;
using CooKit.Models.Steps;

namespace CooKit.Models.Editor.Recipes
{
    public interface IEditorRecipe : IRecipe, IEditorModel
    {
        ObservableCollection<string> ObservableImages { get; set; }
        ObservableCollection<IPictogram> ObservablePictograms { get; set; }

        ObservableCollection<IEditorIngredient> ObservableIngredients { get; set; }
        ObservableCollection<IStep> ObservableSteps { get; set; }
    }
}
