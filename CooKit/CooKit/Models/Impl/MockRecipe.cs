using System.Collections.Generic;
using Xamarin.Forms;

namespace CooKit.Models.Impl
{
    public sealed class MockRecipe : IRecipe
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ImageSource MainImage { get; set; }

        public IReadOnlyList<IPictogram> Pictograms { get; set; }

        public IReadOnlyList<IIngredient> Ingredients { get; set; }
        public IReadOnlyList<string> Steps { get; set; }

        public static MockRecipe Example = new MockRecipe(
            "Mock Recipe",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas non sapien ante. Curabitur sit amet ullamcorper quam, euismod pharetra sem. Pellentesque turpis massa, egestas at dignissim et, sagittis a erat. Nulla quis blandit arcu, ac lobortis ligula. Nam lectus ante, vehicula et nisl auctor, pretium vehicula arcu.",
            ImageSource.FromFile("food.jpg"),
            new []{ MockPictogram.Example, MockPictogram.Example },
            new []{ MockIngredient.Example, MockIngredient.Example, MockIngredient.Example },
            new []{ "Mock Step 1", "Mock Step 2", "Mock Step 3"});

        public MockRecipe(
            string name, string description, ImageSource mainImage, 
            IReadOnlyList<IPictogram> pictograms,
            IReadOnlyList<IIngredient> ingredients, IReadOnlyList<string> steps)
        {
            Name = name;
            Description = description;
            MainImage = mainImage;

            Pictograms = pictograms;

            Ingredients = ingredients;
            Steps = steps;
        }
    }
}
