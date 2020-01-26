using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CooKit.Models.Recipes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CooKit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestView
    {
        public TestView()
        {
            InitializeComponent();

            var recipes = new List<IRecipe>();

            for (var i = 0; i < 10; i++)
                recipes.Add(new MockRecipe());

            RecipeView.ItemsSource = recipes;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine((e.CurrentSelection.FirstOrDefault() as IRecipe)?.Name);
        }
    }
}