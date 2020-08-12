namespace CooKit.Mobile.Models.Images
{
    public class Image
    {
        public ImageType Type { get; set; }
        public string Data { get; set; }

        public Image()
        {
        }

        public Image(ImageType type, string data)
        {
            Type = type;
            Data = data;
        }
    }
}
