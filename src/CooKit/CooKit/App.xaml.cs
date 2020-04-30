﻿using System.Reflection;
using Autofac;
using CooKit.Strategies.Initialization.App;
using CooKit.Views.Root;
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

            var initStrategy = container.Resolve<IAppInitializationStrategy>();
            try
            {
                await initStrategy.InitializeApp(container);
            }
            catch (System.Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                throw;
            }
        }

        private static IContainer BuildIoC()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(assembly);

            return builder.Build();
        }
    }
}
