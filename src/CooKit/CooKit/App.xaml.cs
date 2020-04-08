using CooKit.ViewModels;

namespace CooKit
{
    public partial class App
    {
        public App() => 
            InitializeComponent();

        protected override void OnStart()
        {
            base.OnStart();

            var container = Bootstrap.BuildIoC();
            ViewModelLocator.Initialize(container);

            MainPage = new AppShell();
        }
    }
}
