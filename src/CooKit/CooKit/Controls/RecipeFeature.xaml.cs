using System.Windows.Input;
using CooKit.Models.Recipes;
using Xamarin.Forms;

namespace CooKit.Controls
{
    public partial class RecipeFeature
    {
        public static readonly BindableProperty RecipeProperty =
            BindableProperty.Create(nameof(Recipe), typeof(IRecipe), typeof(RecipePreview), propertyChanged: OnRecipeChanged);

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(RecipePreview), propertyChanged: OnCommandChanged);

        private static void OnRecipeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is RecipeFeature feature))
                return;

            var recipe = newValue as IRecipe;

            feature.RecipeImage.Source = recipe?.PreviewImage;
            feature.WrappingCard.ClickCommandParameter = recipe;
        }

        private static void OnCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is RecipeFeature feature))
                return;

            feature.WrappingCard.ClickCommand = newValue as ICommand;
        }

        public RecipeFeature()
        {
            InitializeComponent();
        }

        public IRecipe Recipe
        {
            get => (IRecipe) GetValue(RecipeProperty);
            set => SetValue(RecipeProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
    }
}