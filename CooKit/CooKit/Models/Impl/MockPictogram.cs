using Xamarin.Forms;

namespace CooKit.Models.Impl
{
    public sealed class MockPictogram : IPictogram
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ImageSource Image { get; set; }

        public static MockPictogram Example = new MockPictogram(
            "Mock Pictogram",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            ImageSource.FromFile("breakfast.png"));

        public MockPictogram(string name, string description, ImageSource image)
        {
            Name = name;
            Description = description;
            Image = image;
        }
    }
}
