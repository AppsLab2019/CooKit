using System.Windows.Input;
using Xamarin.Forms;

namespace CooKit.Controls
{
    public partial class RecipePreview
    {
        public static readonly BindableProperty NameProperty =
            BindableProperty.Create(nameof(Name), typeof(string), typeof(RecipePreview), propertyChanged: OnNameChanged);

        public static readonly BindableProperty ImageProperty = 
            BindableProperty.Create(nameof(Image), typeof(string), typeof(RecipePreview), propertyChanged: OnImageChanged);

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(RecipePreview), propertyChanged: OnCommandChanged);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(RecipePreview), propertyChanged: OnCommandParameterChanged);

        private static void OnNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is RecipePreview preview))
                return;

            preview.NameLabel.Text = newValue as string;
        }

        private static void OnImageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is RecipePreview preview))
                return;

            preview.PreviewImage.Source = newValue as string;
        }

        private static void OnCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is RecipePreview preview))
                return;

            preview.WrappingCard.ClickCommand = newValue as ICommand;
        }

        private static void OnCommandParameterChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is RecipePreview preview))
                return;

            preview.WrappingCard.ClickCommandParameter = newValue;
        }

        public RecipePreview() => 
            InitializeComponent();

        public string Name
        {
            get => (string) GetValue(NameProperty);
            set => SetValue(NameProperty, value);
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