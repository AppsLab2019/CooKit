using System.Collections;
using Xamarin.Forms;

namespace CooKit.Controls
{
    public partial class IngredientsCard
    {
        public static readonly BindableProperty IngredientsProperty =
            BindableProperty.Create(nameof(Ingredients), typeof(IEnumerable), typeof(IngredientsCard), propertyChanged: OnIngredientsChanged);

        private static void OnIngredientsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is IngredientsCard card))
                return;

            var layout = card.IngredientsLayout;
            var ingredients = newValue as IEnumerable;
            BindableLayout.SetItemsSource(layout, ingredients);
        }

        public IngredientsCard() => 
            InitializeComponent();

        public IEnumerable Ingredients
        {
            get => (IEnumerable) GetValue(IngredientsProperty);
            set => SetValue(IngredientsProperty, value);
        }
    }
}