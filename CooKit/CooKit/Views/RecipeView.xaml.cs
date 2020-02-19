using System;
using CooKit.Models;
using CooKit.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CooKit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeView
    {
        public RecipeView(RecipeViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }

        // TODO: completely rewrite this solution
        private void OnPictogramClicked(object sender, EventArgs e)
        {
            var pictogram = (IPictogram)((ImageButton)sender).BindingContext;

            DisplayAlert(pictogram.Name, pictogram.Description, "Okay");
        }
    }
}