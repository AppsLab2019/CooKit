using System;
using AutoMapper;
using CooKit.Mobile.Contexts;
using CooKit.Mobile.Extensions;
using CooKit.Mobile.Factories.Page;
using CooKit.Mobile.Factories.Page.Root;
using CooKit.Mobile.Pages.Lists;
using CooKit.Mobile.Pages.Master;
using CooKit.Mobile.Pages.Recipes;
using CooKit.Mobile.Profiles;
using CooKit.Mobile.Providers.DatabasePath;
using CooKit.Mobile.Providers.Page.CurrentRoot;
using CooKit.Mobile.Providers.Page.Main;
using CooKit.Mobile.Providers.Page.MasterDetail;
using CooKit.Mobile.Registries.PageViewmodel;
using CooKit.Mobile.Repositories.Pictograms;
using CooKit.Mobile.Repositories.Recipes;
using CooKit.Mobile.Resources;
using CooKit.Mobile.Resources.Database;
using CooKit.Mobile.Selectors.Steps;
using CooKit.Mobile.Services.Alert;
using CooKit.Mobile.Services.Database;
using CooKit.Mobile.Services.Injector;
using CooKit.Mobile.Services.Navigation;
using CooKit.Mobile.Services.Root;
using CooKit.Mobile.Viewmodels.Lists;
using CooKit.Mobile.Viewmodels.Master;
using CooKit.Mobile.Viewmodels.Recipes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xamarin.Forms;

namespace CooKit.Mobile.Factories
{
    public static class ServiceProviderFactory
    {
        public static IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddConfiguredLogging();
            services.AddConfiguredRecipeContext();
            services.AddConfiguredAutoMapper();

            services.AddPages();
            services.AddViewModels();
            services.AddFactories();
            services.AddProviders();
            services.AddRepositories();
            services.AddServices();
            services.AddSelectors();
            services.AddRegistries();

            services.AddSingleton<Application, App>();

            return services.BuildServiceProvider();
        }

        private static void AddConfiguredLogging(this IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                // TODO: configure logging
                loggingBuilder.AddDebug();
            });
        }

        private static void AddConfiguredRecipeContext(this IServiceCollection services)
        {
            services.AddDbContext<RecipeContext>((serviceProvider, contextBuilder) =>
            {
                var pathProvider = serviceProvider.GetRequiredService<IDatabasePathProvider>();
                var path = pathProvider.GetDatabasePath();

                // TODO: refactor this out
                contextBuilder.UseSqlite($"Data Source={path}");
                contextBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }

        private static void AddConfiguredAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(mapperBuilder =>
            {
                mapperBuilder.AddProfile<RecipeProfile>();
            });
        }

        private static void AddPages(this IServiceCollection services)
        {
            services.AddTransient<MasterPage>();
            services.AddSingleton<MasterDetailPage>();

            services.AddTransient<LocalRecipeListPage>();
            services.AddTransient<RecipeDetailPage>();
        }

        private static void AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<MasterViewmodel>();

            services.AddTransient<LocalRecipeListViewmodel>();
            services.AddTransient<RecipeDetailViewmodel>();
        }

        private static void AddFactories(this IServiceCollection services)
        {
            services.AddTransient<IPageFactory, ServiceProviderInjectionPageFactory>();
            services.AddTransient<IRootPageFactory, MaterialRootPageFactory>();
        }

        private static void AddProviders(this IServiceCollection services)
        {
            services.AddTransient<IDatabasePathProvider, DatabasePathProvider>();

            services.AddTransient<IMainPageProvider, MainPageProvider>();
            services.AddTransient<ICurrentRootPageProvider, CurrentRootPageProvider>();
            services.AddTransient<IMasterDetailPageProvider, ServiceProviderMasterDetailPageProvider>();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IPictogramRepository, PictogramRepository>();
            services.AddTransient<IRecipeRepository, MockRecipeRepository>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAlertService, MaterialAlertService>();
            services.AddTransient<INavigationService, NavigationService>();
            services.AddSingleton<IRootService, CachingRootService>();
            services.AddTransient<IViewModelInjector, PageViewModelInjector>();

            services.AddTransient<IResourceExtractor, ResourceExtractor>();
            services.AddTransient<IDatabaseExtractor, DatabaseExtractor>();
            services.AddTransient<IDatabaseInitializationService, DatabaseInitializationService>();
        }

        private static void AddSelectors(this IServiceCollection services)
        {
            services.AddTransient<RecipeDetailStepTemplateSelector>();
        }

        private static void AddRegistries(this IServiceCollection services)
        {
            services.AddSingleton<IPageViewmodelTypeRegistry, PageViewmodelTypeRegistry>(serviceProvider =>
            {
                var registry = new PageViewmodelTypeRegistry();

                registry.Register<MasterPage, MasterViewmodel>();
                registry.Register<LocalRecipeListPage, LocalRecipeListViewmodel>();
                registry.Register<RecipeDetailPage, RecipeDetailViewmodel>();

                return registry;
            });
        }
    }
}
