using System.Windows.Input;
using Xamarin.Forms;

namespace CooKit.Views.Recipes
{
    public partial class RecipePreview
    {
        public static BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(RecipePreview), propertyChanged: OnCommandChanged);

        private static void OnCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is RecipePreview preview))
                return;

            if (oldValue == newValue)
                return;

            preview.WrappingCard.ClickCommand = newValue as ICommand;
        }

        public RecipePreview() => 
            InitializeComponent();

        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
    }
}