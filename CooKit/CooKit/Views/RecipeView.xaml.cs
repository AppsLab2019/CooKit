using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CooKit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeView
    {
        public RecipeView() => 
            InitializeComponent();

        private void OnBackButtonClicked(object sender, EventArgs e) =>
            Shell.Current.Navigation.PopAsync();
    }

    public sealed class TemporaryRecipeStep
    {
        public string Value { get; set; }
    }
}