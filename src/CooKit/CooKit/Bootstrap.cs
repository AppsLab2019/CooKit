using System;
using Autofac;
using CooKit.Mappers;
using CooKit.Mappers.Converters.Guids;
using CooKit.Mappers.Converters.Uris;
using CooKit.Mappers.Profiles;
using CooKit.Services.Alerts;
using CooKit.Services.Editor;
using CooKit.Services.Recipes;
using CooKit.Services.Repositories.Ingredients;
using CooKit.Services.Repositories.Pictograms;
using CooKit.Services.Repositories.Recipes;
using CooKit.Services.Stores.Ingredients;
using CooKit.Services.Stores.Pictograms;
using CooKit.Services.Stores.Recipes;
using CooKit.ViewModels;

namespace CooKit
{
    public static class Bootstrap
    {
        public static IContainer BuildIoC()
        {
            var builder = new ContainerBuilder();

            RegisterMapper(builder);
            RegisterServices(builder);
            builder.RegisterModule<ViewModelModule>();

            return builder.Build();
        }

        private static void RegisterMapper(ContainerBuilder builder)
        {
            builder.RegisterType<GuidListToStringConverter>().As<IGuidListToStringConverter>();
            builder.RegisterType<StringToGuidListConverter>().As<IStringToGuidListConverter>();

            builder.RegisterType<UriListToStringConverter>().As<IUriListToStringConverter>();
            builder.RegisterType<StringToUriListConverter>().As<IStringToUriListConverter>();

            builder.RegisterType<SQLiteMappingProfile>();

            builder.RegisterType<MapperFactory>().As<IMapperFactory>();
            builder.Register(c => c.Resolve<IMapperFactory>().CreateMapper())
                .As<AutoMapper.IMapper>().SingleInstance();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<AlertService>().As<IAlertService>().SingleInstance();
            builder.RegisterType<EditorService>().As<IEditorService>().SingleInstance();
            builder.RegisterType<RecipeSelectService>().As<IRecipeSelectService>().SingleInstance();

            // TODO: move path resolving to configuration class
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var path = System.IO.Path.Combine(folder, "CooKit.db3");
            var connection = new SQLite.SQLiteAsyncConnection(path);

            builder.RegisterInstance(connection);

            builder.RegisterType<SQLiteIngredientRepository>().As<IIngredientRepository>().SingleInstance();
            builder.RegisterType<SQLitePictogramRepository>().As<IPictogramRepository>().SingleInstance();
            builder.RegisterType<SQLiteRecipeRepository>().As<IRecipeRepository>().SingleInstance();

            // dependencies of SQLiteRecipeRepository
            builder.RegisterType<SQLiteRecipeDtoRepository>().As<ISQLiteRecipeDtoRepository>().SingleInstance();

            builder.RegisterType<IngredientStore>().As<IIngredientStore>().SingleInstance();
            builder.RegisterType<PictogramStore>().As<IPictogramStore>().SingleInstance();
            builder.RegisterType<RecipeStore>().As<IRecipeStore>().SingleInstance();
        }
    }
}
