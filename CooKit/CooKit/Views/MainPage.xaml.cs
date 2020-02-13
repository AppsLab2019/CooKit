using System;
using Xamarin.Forms.Xaml;

namespace CooKit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage
    {
        public MainPage() => 
            InitializeComponent();

        private void OnRecipeTapped(object sender, EventArgs e) =>
            throw new NotImplementedException();
    }
}