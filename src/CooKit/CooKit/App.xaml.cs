using CooKit.ViewModels;

namespace CooKit
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this, "Material.Configuration");
        }

        protected override void OnStart()
        {
            base.OnStart();

            var container = Bootstrap.BuildIoC();
            ViewModelLocator.Initialize(container);

            MainPage = new AppShell();
        }
    }
}
