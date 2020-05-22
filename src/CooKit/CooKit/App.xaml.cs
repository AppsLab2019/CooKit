using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using CooKit.Services.Database;
using CooKit.Services.Navigation;
using CooKit.ViewModels;
using Xamarin.Forms;

namespace CooKit
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this, "Material.Configuration");

            // this is needed if the OnStart method is async
            MainPage = new ContentPage();
        }

        protected override async void OnStart()
        {
            base.OnStart();
            var container = BuildIoC();

            ViewModelLocator.Initialize(container);
            ViewModel.Initialize(container);

            InitializeResources(container);
            await InitializeDatabase(container);
            await InitializeNavigation(container);
        }

        private static IContainer BuildIoC()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(assembly);

            return builder.Build();
        }

        // TODO: move this
        // TODO: dynamic registration through reflection and attributes
        private void InitializeResources(IComponentContext ctx)
        {
            var converter = (Converters.IngredientToFormattedStringConverter) 
                Resources["Converter.IngredientToFormattedStringConverter"];

            var unitService = ctx.Resolve<Services.Units.IUnitService>();
            converter.UnitService = unitService;
        }

        private static Task InitializeDatabase(IComponentContext ctx)
        {
            var initialization = ctx.Resolve<ISQLiteInitialization>();
            return initialization.InitializeAsync();
        }

        private static Task InitializeNavigation(IComponentContext ctx)
        {
            var navigation = ctx.Resolve<INavigationService>();
            return navigation.InitializeAsync();
        }
    }
}
