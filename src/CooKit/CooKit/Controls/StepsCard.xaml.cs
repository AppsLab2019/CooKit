using System.Collections;
using Xamarin.Forms;

namespace CooKit.Controls
{
    public partial class StepsCard
    {
        public static readonly BindableProperty StepsProperty =
            BindableProperty.Create(nameof(Steps), typeof(IEnumerable), typeof(StepsCard), propertyChanged: OnStepsChanged);

        private static void OnStepsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is StepsCard card))
                return;

            card.StepCarousel.ItemsSource = newValue as IEnumerable;
        }

        public StepsCard() => 
            InitializeComponent();

        public IEnumerable Steps
        {
            get => (IEnumerable) GetValue(StepsProperty);
            set => SetValue(StepsProperty, value);
        }
    }
}