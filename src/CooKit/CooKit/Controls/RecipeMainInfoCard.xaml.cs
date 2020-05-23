using Xamarin.Forms;

namespace CooKit.Controls
{
    public partial class RecipeMainInfoCard
    {
        public static readonly BindableProperty NameProperty =
            BindableProperty.Create(nameof(Name), typeof(string), typeof(RecipeMainInfoCard), propertyChanged: OnNameChanged);

        public static readonly BindableProperty DescriptionProperty =
            BindableProperty.Create(nameof(Description), typeof(string), typeof(RecipeMainInfoCard), propertyChanged: OnDescriptionChanged);

        private static void OnNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is RecipeMainInfoCard card))
                return;

            card.NameLabel.Text = newValue as string;
        }

        private static void OnDescriptionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is RecipeMainInfoCard card))
                return;

            card.DescriptionLabel.Text = newValue as string;
        }

        public RecipeMainInfoCard() => 
            InitializeComponent();

        public string Name
        {
            get => (string) GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }

        public string Description
        {
            get => (string) GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }
    }
}