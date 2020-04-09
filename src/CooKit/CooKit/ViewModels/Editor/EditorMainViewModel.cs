using System.Collections.Generic;
using System.Linq;
using CooKit.Models;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditorMainViewModel : BaseViewModel
    {
        #region Recipe Properties

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int EstimatedTime { get; private set; }
        public IEnumerable<string> Images { get; private set; }
        public IEnumerable<Pictogram> Pictograms { get; private set; }
        public IEnumerable<Ingredient> Ingredients { get; private set; }

        #endregion

        public EditorMainViewModel()
        {
            Name = "Test Recipe";
            Description = "Test description lorem ipsum lmao sdfsd gfdhgfds hxvcbxcvnb gf hgfhfgh.";
            EstimatedTime = 10;

            Images = Enumerable.Repeat("http://www.eduka.eu.sk/wp-content/uploads/2019/07/food.jpg", 5);

            Pictograms = Enumerable.Repeat(new Pictogram
            {
                Name = "Test Pictogram",
                Description = "Test Description",
                Icon = "breakfast.png"
            }, 3);

            Ingredients = Enumerable.Repeat(new Ingredient
            {
                Name = "Test Ingredient",
                Icon = "breakfast.png"
            }, 4);
        }
    }
}
