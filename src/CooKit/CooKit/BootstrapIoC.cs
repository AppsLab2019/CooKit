using System;
using Autofac;
using CooKit.Mappers;
using CooKit.Services.Alerts;
using CooKit.Services.Repositories.Ingredients;
using CooKit.Services.Repositories.Pictograms;
using CooKit.Services.Repositories.Recipes;
using CooKit.ViewModels.Recipes;
using CooKit.Views.Recipes;

namespace CooKit
{
    public static class BootstrapIoC
    {
        public static IContainer BuildIoC()
        {
            var builder = new ContainerBuilder();

            RegisterMapper(builder);
            RegisterServices(builder);
            RegisterViews(builder);
            RegisterViewModels(builder);

            return builder.Build();
        }

        private static void RegisterMapper(ContainerBuilder builder)
        {
            builder.RegisterType<MapperFactory>().As<IMapperFactory>();
            builder.Register(c => c.Resolve<IMapperFactory>().CreateMapper())
                .As<AutoMapper.IMapper>().SingleInstance();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<AlertService>().As<IAlertService>();

            // TODO: move path resolving to configuration class
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var path = System.IO.Path.Combine(folder, "CooKit.db3");
            var connection = new SQLite.SQLiteAsyncConnection(path);

            builder.RegisterInstance(connection);

            builder.RegisterType<SQLiteIngredientRepository>().As<IIngredientRepository>();
            builder.RegisterType<SQLitePictogramRepository>().As<IPictogramRepository>();
            builder.RegisterType<SQLiteRecipeRepository>().As<IRecipeRepository>();

            // dependencies of SQLiteRecipeRepository
            builder.RegisterType<SQLiteRecipeDtoRepository>().As<ISQLiteRecipeDtoRepository>();
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
