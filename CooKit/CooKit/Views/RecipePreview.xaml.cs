using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CooKit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipePreview
    {

        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create("Image", typeof(ImageSource), typeof(RecipePreview));

        public static readonly BindableProperty NameProperty =
            BindableProperty.Create("Name", typeof(string), typeof(RecipePreview));

        public ImageSource Image
        {
            get => (ImageSource) GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public string Name
        {
            get => (string) GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }

        public RecipePreview()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext == null) 
                return;

            BackgroundImage.Source = Image;
            NameLabel.Text = Name;
        }
    }
}