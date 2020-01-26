using CooKit.Models.Recipes;
using CooKit.ViewModels;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CooKit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeIntroduction : ContentPage
    {
        public RecipeIntroduction([NotNull] IRecipe recipe)
        {
            InitializeComponent();
            BindingContext = new RecipeIntroductionViewModel(recipe);
        }
    }
}