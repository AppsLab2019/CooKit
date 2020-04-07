using System;
using CooKit.ViewModels.Recipes;

namespace CooKit.Views.Recipes
{
    public partial class RecipeListView
    {
        private readonly IRecipeListViewModel _viewModel;

        public RecipeListView(IRecipeListViewModel viewModel)
        {
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));

            InitializeComponent();
            _viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.RefreshCommand.Execute(null);
        }
    }
}