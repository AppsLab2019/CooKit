using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace CooKit.Controls
{
    public partial class EditListCard
    {
        public static readonly BindableProperty HeaderTextProperty =
            BindableProperty.Create(nameof(HeaderText), typeof(string), typeof(EditListCard), propertyChanged: OnHeaderChanged);

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(EditListCard), propertyChanged: OnSourceChanged);

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(EditListCard), propertyChanged: OnTemplateChanged);

        public static readonly BindableProperty AddCommandProperty =
            BindableProperty.Create(nameof(AddCommand), typeof(ICommand), typeof(EditListCard), propertyChanged: OnAddCommandChanged);

        private static void OnHeaderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is EditListCard card))
                return;

            card.HeaderLabel.Text = newValue as string;
        }

        private static void OnSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is EditListCard card))
                return;

            card.WrappedListView.ItemsSource = newValue as IEnumerable;
        }

        private static void OnTemplateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is EditListCard card))
                return;

            card.WrappedListView.ItemTemplate = newValue as DataTemplate;
        }

        private static void OnAddCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is EditListCard card))
                return;

            card.AddButton.Command = newValue as ICommand;
        }

        public EditListCard() => 
            InitializeComponent();

        public string HeaderText
        {
            get => (string) GetValue(HeaderTextProperty);
            set => SetValue(HeaderTextProperty, value);
        }

        public IEnumerable ItemsSource
        {
            get => (IEnumerable) GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate) GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public ICommand AddCommand
        {
            get => (ICommand) GetValue(AddCommandProperty);
            set => SetValue(AddCommandProperty, value);
        }
    }
}