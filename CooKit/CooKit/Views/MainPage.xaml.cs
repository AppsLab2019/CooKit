using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CooKit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage
    {
        public MainPage() => 
            InitializeComponent();

        private void OnRecipeTapped(object sender, EventArgs e) =>
            Shell.Current.GoToAsync("introduction");
    }
}