using System.Collections.ObjectModel;

namespace CooKit.Models
{
    public interface IRecipeTemplate
    {
        string Name { get; set; }
        string Description { get; set; }
        int EstimatedTime { get; set; }

        ObservableCollection<string> Images { get; set; }
        ObservableCollection<Pictogram> Pictograms { get; set; }
        ObservableCollection<Ingredient> Ingredients { get; set; }
    }
}
