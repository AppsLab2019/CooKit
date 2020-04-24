using System;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using CooKit.Extensions;
using CooKit.ViewModels;
using SQLite;
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
            // TODO: investigate this behavior
            MainPage = new ContentPage();
        }

        protected override async void OnStart()
        {
            base.OnStart();

            var container = BuildIoC();
            ViewModelLocator.Initialize(container);

            await InitDb(container);

            MainPage = new AppShell();
        }

        private static IContainer BuildIoC()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(assembly);

            return builder.Build();
        }

        [Obsolete("Initialization class should be used!")]
        private static async Task InitDb(IContainer container)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();

            var lazyConnection = container.LazyResolve<SQLiteAsyncConnection>();

            foreach (var type in types)
            {
                var attribute = type.GetCustomAttribute<TableAttribute>();

                if (attribute is null)
                    continue;

                await lazyConnection.Value.CreateTableAsync(type);
            }
        }
    }
}
