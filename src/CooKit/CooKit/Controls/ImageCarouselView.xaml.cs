using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace CooKit.Controls
{
    public partial class ImageCarouselView
    {
        public static readonly BindableProperty ImagesProperty =
            BindableProperty.Create(nameof(Images), typeof(IEnumerable), typeof(ImageCarouselView), propertyChanged: OnImagesChanged);

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ImageCarouselView));

        public static readonly BindableProperty IsClickableProperty = 
            BindableProperty.Create(nameof(IsClickable), typeof(bool), typeof(ImageCarouselView), false);

        private static void OnImagesChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is ImageCarouselView view))
                return;

            view.ImageCarousel.ItemsSource = newValue as IEnumerable;
        }

        public ImageCarouselView() => 
            InitializeComponent();

        public IEnumerable Images
        {
            get => (IEnumerable) GetValue(ImagesProperty);
            set => SetValue(ImagesProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public bool IsClickable
        {
            get => (bool) GetValue(IsClickableProperty);
            set => SetValue(IsClickableProperty, value);
        }
    }
}