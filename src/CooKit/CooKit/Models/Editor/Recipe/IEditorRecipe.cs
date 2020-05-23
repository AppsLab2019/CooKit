using System.Collections.ObjectModel;
using System.ComponentModel;
using CooKit.Models.Ingredients;
using CooKit.Models.Pictograms;
using CooKit.Models.Recipes;
using CooKit.Models.Steps;

namespace CooKit.Models.Editor.Recipe
{
    public interface IEditorRecipe : IRecipe, INotifyPropertyChanged
    {
        ObservableCollection<string> ObservableImages { get; set; }
        ObservableCollection<IIngredient> ObservableIngredients { get; set; }
        ObservableCollection<IPictogram> ObservablePictograms { get; set; }
        ObservableCollection<IStep> ObservableSteps { get; set; }
    }
}
