using Autofac;
using CooKit.Services.Alerts;
using CooKit.ViewModels.Recipes;
using CooKit.Views.Recipes;

namespace CooKit
{
    public static class BootstrapIoC
    {
        public static IContainer BuildIoC()
        {
            var builder = new ContainerBuilder();

            RegisterServices(builder);
            RegisterViews(builder);
            RegisterViewModels(builder);

            return builder.Build();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<AlertService>().As<IAlertService>();
        }

        private static void RegisterViews(ContainerBuilder builder)
        {
            builder.RegisterType<RecipeListView>();
        }

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<RecipeListViewModel>().As<IRecipeListViewModel>();
        }
    }
}
