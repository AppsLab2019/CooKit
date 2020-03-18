using System.Collections;
using Xamarin.Forms;

namespace CooKit.Views.Editor.Designers
{
    public partial class ImageSelector
    {
        public static BindableProperty LoaderProperty = 
            BindableProperty.Create(nameof(Loader), typeof(string), typeof(ImageSelector), defaultBindingMode: BindingMode.TwoWay);

        public static BindableProperty SourceProperty =
            BindableProperty.Create(nameof(Source), typeof(string), typeof(ImageSelector), defaultBindingMode: BindingMode.TwoWay);

        public static BindableProperty AvailableLoadersProperty = 
            BindableProperty.Create(nameof(AvailableLoaders), typeof(IList), typeof(ImageSelector));

        public ImageSelector() => 
            InitializeComponent();

        public string Loader
        {
            get => (string) GetValue(LoaderProperty);
            set => SetValue(LoaderProperty, value);
        }

        public string Source
        {
            get => (string) GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public IList AvailableLoaders
        {
            get => (IList) GetValue(AvailableLoadersProperty);
            set => SetValue(AvailableLoadersProperty, value);
        }
    }
}