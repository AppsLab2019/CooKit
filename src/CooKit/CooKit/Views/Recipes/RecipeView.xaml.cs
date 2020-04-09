using CooKit.ViewModels.Recipes;

namespace CooKit.Views.Recipes
{
    public partial class RecipeView
    {
        public RecipeView() => 
            InitializeComponent();

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is RecipeViewModel vm)
                vm.UpdateCommand.Execute(null);
        }
    }
}