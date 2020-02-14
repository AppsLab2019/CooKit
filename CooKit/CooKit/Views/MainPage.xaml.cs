using System;
using CooKit.Models;
using CooKit.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CooKit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage
    {
        public MainPage() => 
            InitializeComponent();

        // TODO: completely rewrite this solution
        private void OnRecipeTapped(object sender, EventArgs e) =>
            Shell.Current.Navigation.PushAsync(new RecipeIntroduction(
                new RecipeViewModel((IRecipe) ((StackLayout) sender).BindingContext)));
    }
}