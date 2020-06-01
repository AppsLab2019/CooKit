using System.Windows.Input;
using Xamarin.Forms;

namespace CooKit.Controls
{
    public partial class RecipeFeature
    {
        public static readonly BindableProperty NameProperty =
            BindableProperty.Create(nameof(Name), typeof(string), typeof(RecipePreview), propertyChanged: OnNameChanged);

        public static readonly BindableProperty DescriptionProperty =
            BindableProperty.Create(nameof(Description), typeof(string), typeof(RecipePreview), propertyChanged: OnDescriptionChanged);

        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(RecipePreview), propertyChanged: OnImageChanged);

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(RecipePreview), propertyChanged: OnCommandChanged);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(RecipeFeature), propertyChanged: OnCommandParameterChanged);

        private static void OnNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is RecipeFeature feature))
                return;

            feature.NameLabel.Text = newValue as string;
        }

        private static void OnDescriptionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is RecipeFeature feature))
                return;

            feature.DescriptionLabel.Text = newValue as string;
        }

        private static void OnImageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is RecipeFeature feature))
                return;

            feature.RecipeImage.Source = newValue as string;
        }

        private static void OnCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is RecipeFeature feature))
                return;

            feature.WrappingCard.ClickCommand = newValue as ICommand;
        }

        private static void OnCommandParameterChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is RecipeFeature feature))
                return;

            feature.WrappingCard.ClickCommandParameter = newValue;
        }

        public RecipeFeature()
        {
            InitializeComponent();
        }

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

        public string Image
        {
            get => (string) GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
    }
}