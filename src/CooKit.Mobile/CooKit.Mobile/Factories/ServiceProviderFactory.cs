using System;
using CooKit.Mobile.Services.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace CooKit.Mobile.Factories
{
    public static class ServiceProviderFactory
    {
        public static IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddConfiguredLogging();
            services.AddServices();

            services.AddSingleton<Application, App>();

            return services.BuildServiceProvider();
        }

        private static void AddConfiguredLogging(this IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                // TODO: configure logging
            });
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<INavigationService, NavigationService>();
        }
    }
}
