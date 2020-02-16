using Xamarin.Forms;

namespace CooKit.Models
{
    public sealed class MockIngredient : IIngredient
    {
        public string Name { get; }
        public ImageSource Icon { get; }

        public static MockIngredient Example = new MockIngredient(
            "Mock Ingredient",
            ImageSource.FromFile("breakfast.png"));

        public MockIngredient(string name, ImageSource icon)
        {
            Name = name;
            Icon = icon;
        }
    }
}
