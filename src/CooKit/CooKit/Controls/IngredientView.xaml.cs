using System.Text;
using Xamarin.Forms;

namespace CooKit.Controls
{
    public partial class IngredientView
    {
        public static readonly BindableProperty IconProperty =
            BindableProperty.Create(nameof(Icon), typeof(string), typeof(IngredientView), propertyChanged: OnIconChanged);

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(string), typeof(IngredientView), propertyChanged: OnTextChanged);

        public static readonly BindableProperty NameProperty =
            BindableProperty.Create(nameof(Name), typeof(string), typeof(IngredientView), propertyChanged: OnTextChanged);

        public static readonly BindableProperty NoteProperty =
            BindableProperty.Create(nameof(Note), typeof(string), typeof(IngredientView), propertyChanged: OnTextChanged);

        private static void OnIconChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is IngredientView view))
                return;

            view.IconImage.Source = newValue as string;
        }

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is IngredientView view))
                return;

            view.RefreshLabel();
        }

        public IngredientView() => 
            InitializeComponent();

        // TODO: remove trailing spaces
        private void RefreshLabel()
        {
            var builder = new StringBuilder();

            if (!string.IsNullOrEmpty(Value))
            {
                builder.Append(Value);
                builder.Append(' ');
            }

            if (!string.IsNullOrEmpty(Name))
            {
                builder.Append(Name);
                builder.Append(' ');
            }

            if (!string.IsNullOrEmpty(Note))
            {
                builder.Append('(');
                builder.Append(Note);
                builder.Append(')');
            }

            TextLabel.Text = builder.ToString();
        }

        public string Icon
        {
            get => (string) GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public string Value
        {
            get => (string) GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
    }

        public string Name
        {
            get => (string) GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }

        public string Note
        {
            get => (string) GetValue(NoteProperty);
            set => SetValue(NoteProperty, value);
        }
    }
}