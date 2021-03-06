﻿using System;
using CooKit.Mobile.Contexts;
using CooKit.Mobile.Converters;
using CooKit.Mobile.Extensions;
using CooKit.Mobile.Factories.Page;
using CooKit.Mobile.Factories.Page.Root;
using CooKit.Mobile.Factories.Views;
using CooKit.Mobile.Models.Root;
using CooKit.Mobile.Pages;
using CooKit.Mobile.Providers.Categories;
using CooKit.Mobile.Providers.DatabasePath;
using CooKit.Mobile.Providers.Page.CurrentRoot;
using CooKit.Mobile.Providers.Page.Main;
using CooKit.Mobile.Providers.Page.MasterDetail;
using CooKit.Mobile.Providers.ResourcePath;
using CooKit.Mobile.Registries.PageViewmodel;
using CooKit.Mobile.Registries.RootEntry;
using CooKit.Mobile.Repositories.Pictograms;
using CooKit.Mobile.Repositories.Recipes;
using CooKit.Mobile.Resources;
using CooKit.Mobile.Resources.Database;
using CooKit.Mobile.Selectors;
using CooKit.Mobile.Selectors.Steps;
using CooKit.Mobile.Services.Alert;
using CooKit.Mobile.Services.Alert.Loading;
using CooKit.Mobile.Services.Database;
using CooKit.Mobile.Services.Feature;
using CooKit.Mobile.Services.Injector;
using CooKit.Mobile.Services.Navigation;
using CooKit.Mobile.Services.Pickers;
using CooKit.Mobile.Services.Publish;
using CooKit.Mobile.Services.Resources;
using CooKit.Mobile.Services.Root;
using CooKit.Mobile.Viewmodels;
using CooKit.Mobile.Views.Editor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xamarin.Forms;

namespace CooKit.Mobile
{
    public static class Bootstrap
    {
        public static IServiceProvider CreateServiceProvider()
        {
            return CreateServiceProvider(null);
        }

        public static IServiceProvider CreateServiceProvider(Action<IServiceCollection> registrationAction)
        {
            var services = new ServiceCollection();

            services.AddConfiguredLogging();
            services.AddConfiguredRecipeContext();

            services.AddPages();
            services.AddViewModels();
            services.AddFactories();
            services.AddProviders();
            services.AddRepositories();
            services.AddServices();
            services.AddSelectors();
            services.AddRegistries();
            services.AddViews();

            services.AddTransient<ImageToImageSourceConverter>();
            services.AddSingleton<Application, App>();

            registrationAction?.Invoke(services);
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

        private static void AddPages(this IServiceCollection services)
        {
            services.AddTransient<MasterPage>();
            services.AddSingleton<MasterDetailPage>();

            services.AddTransient<TitlePage>();
            services.AddTransient<AboutPage>();
            services.AddTransient<RecipeListPage>();

            services.AddTransient(serviceProvider =>
            {
                var selector = serviceProvider.GetRequiredService<RecipeDetailStepTemplateSelector>();
                return new RecipeDetailPage(selector);
            });

            services.AddTransient(serviceProvider =>
            {
                var viewFactory = serviceProvider.GetRequiredService<IViewFactory>();
                var selector = serviceProvider.GetRequiredService<ReturnIfTemplateSelector>();
                return new EditorMainPage(viewFactory, selector);
            });
        }

        private static void AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<MasterViewmodel>();
            services.AddTransient<TitleViewmodel>();
            services.AddTransient<AboutViewmodel>();
            services.AddTransient<RecipeListViewmodel>();
            services.AddTransient<RecipeDetailViewmodel>();
            services.AddTransient<EditorMainViewmodel>();
        }

        private static void AddFactories(this IServiceCollection services)
        {
            services.AddTransient<IViewFactory, ServiceProviderViewFactory>();
            services.AddTransient<IPageFactory, ServiceProviderInjectionPageFactory>();
            services.AddTransient<IRootPageFactory, MaterialRootPageFactory>();
        }

        private static void AddProviders(this IServiceCollection services)
        {
            services.AddTransient<IDatabasePathProvider, DatabasePathProvider>();
            services.AddTransient<IResourcePathProvider, ResourcePathProvider>();
            services.AddTransient<ICustomResourceProvider, AssemblyCustomResourceProvider>();

            services.AddTransient<IMainPageProvider, MainPageProvider>();
            services.AddTransient<ICurrentRootPageProvider, CurrentRootPageProvider>();
            services.AddTransient<IMasterDetailPageProvider, ServiceProviderMasterDetailPageProvider>();

            services.AddTransient<ICategoryProvider, MockCategoryProvider>();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IPictogramRepository, PictogramRepository>();
            services.AddTransient<IRecipeRepository, RecipeRepository>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAlertService, MaterialAlertService>();
            services.AddTransient<ILoadingAlertService, MaterialLoadingAlertService>();
            services.AddTransient<INavigationService, NavigationService>();
            services.AddTransient<IRootService, CachingRootService>();
            services.AddTransient<IViewModelInjector, PageViewModelInjector>();
            services.AddTransient<IPublishService, LocalPublishService>();
            services.AddTransient<IImagePicker, MediaPluginImagePicker>();
            services.AddTransient<IFeatureService, MockFeatureService>();

            // TODO: rename IResourceExtractor to IEmbeddedResourceExtractor
            services.AddTransient<IResourceExtractor, ResourceExtractor>();
            services.AddTransient<IDatabaseExtractor, DatabaseExtractor>();
            services.AddTransient<IDatabaseInitializationService, EnsuringDatabaseInitializationService>();
            services.AddTransient<IResourceInitializationService, ResourceInitializationService>();
        }

        private static void AddSelectors(this IServiceCollection services)
        {
            services.AddTransient<RecipeDetailStepTemplateSelector>();
            services.AddTransient<ReturnIfTemplateSelector>();
        }

        private static void AddRegistries(this IServiceCollection services)
        {
            services.AddSingleton<IPageViewmodelTypeRegistry, PageViewmodelTypeRegistry>(_ =>
            {
                var registry = new PageViewmodelTypeRegistry();

                registry.Register<MasterPage, MasterViewmodel>();
                registry.Register<TitlePage, TitleViewmodel>();
                registry.Register<AboutPage, AboutViewmodel>();
                registry.Register<RecipeListPage, RecipeListViewmodel>();
                registry.Register<RecipeDetailPage, RecipeDetailViewmodel>();
                registry.Register<EditorMainPage, EditorMainViewmodel>();

                return registry;
            });

            services.AddSingleton<IRootEntryRegistry, RootEntryRegistry>(_ =>
            {
                var registry = new RootEntryRegistry();

                registry.Register(new RootEntry(null, "Recipes", typeof(TitleViewmodel)));
                registry.Register(new RootEntry(null, "About", typeof(AboutViewmodel)));

                return registry;
            });
        }

        private static void AddViews(this IServiceCollection services)
        {
            services.AddTransient<EditorNameView>();
            services.AddTransient<EditorDescriptionView>();
            services.AddTransient<EditorImageView>();
            services.AddTransient<EditorPictogramView>();
            services.AddTransient<EditorIngredientView>();
            services.AddTransient<EditorStepView>();
        }
    }
}
