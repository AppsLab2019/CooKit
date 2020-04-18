using System.Collections.ObjectModel;

namespace CooKit.Models
{
    public sealed class RecipeTemplate : IRecipeTemplate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int EstimatedTime { get; set; }
        public ObservableCollection<string> Images { get; set; }
        public ObservableCollection<Pictogram> Pictograms { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }
    }
}
