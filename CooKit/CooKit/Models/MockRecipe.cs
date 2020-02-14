using Xamarin.Forms;

namespace CooKit.Models
{
    public sealed class MockRecipe : IRecipe
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ImageSource MainImage { get; set; }

        public static MockRecipe Example = new MockRecipe("Mock Recipe",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas non sapien ante. Curabitur sit amet ullamcorper quam, euismod pharetra sem. Pellentesque turpis massa, egestas at dignissim et, sagittis a erat. Nulla quis blandit arcu, ac lobortis ligula. Nam lectus ante, vehicula et nisl auctor, pretium vehicula arcu.",
            ImageSource.FromFile("food.jpg"));

        public MockRecipe(string name, string description, ImageSource mainImage)
        {
            Name = name;
            Description = description;
            MainImage = mainImage;
        }
    }
}
