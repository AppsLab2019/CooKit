using CooKit.Mobile.Extensions;
using CooKit.Mobile.Models;
using Xamarin.Forms;

namespace CooKit.Mobile.Controls.Recipes
{
    public partial class SmallRecipePreviewCard
    {
        public static readonly BindableProperty RecipeProperty =
            BindableProperty.Create(nameof(Recipe), typeof(Recipe), typeof(SmallRecipePreviewCard), propertyChanged: OnRecipeChanged);

        private static void OnRecipeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == newValue)
                return;

            var card = (SmallRecipePreviewCard) bindable;
            var recipe = newValue as Recipe;

            card.NameLabel.Text = recipe?.Name;
            card.PreviewImage.Source = recipe?.PreviewImage?.ToImageSource();
            card.ClickCommandParameter = recipe;
        }

        public SmallRecipePreviewCard()
        {
            InitializeComponent();
        }

        public Recipe Recipe
        {
            get => (Recipe) GetValue(RecipeProperty);
            set => SetValue(RecipeProperty, value);
        }
    }
}