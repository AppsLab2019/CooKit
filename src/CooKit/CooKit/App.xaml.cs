using Autofac;
using CooKit.Views.Recipes;

namespace CooKit
{
    public partial class App
    {
        public App() => 
            InitializeComponent();

        protected override void OnStart()
        {
            base.OnStart();

            var container = BootstrapIoC.BuildIoC();
            MainPage = container.Resolve<RecipeListView>();
        }
    }
}
