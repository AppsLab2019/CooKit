using CooKit.ViewModels.Recipes;

namespace CooKit.Views.Recipes
{
    public partial class RecipeListView
    {
        public RecipeListView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            if (BindingContext is RecipeListViewModel vm)
                vm.RefreshCommand.Execute(null);
        }
    }
}