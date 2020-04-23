using System;
using System.Reflection;
using Autofac;

namespace CooKit
{
    public static class Bootstrap
    {
        public static IContainer BuildIoC()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var builder = new ContainerBuilder();

            RegisterDbConnection(builder);
            builder.RegisterAssemblyModules(assembly);

            return builder.Build();
        }

        private static void RegisterDbConnection(ContainerBuilder builder)
        {
            // TODO: move path resolving to configuration class
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var path = System.IO.Path.Combine(folder, "CooKit.db3");
            var connection = new SQLite.SQLiteAsyncConnection(path);

            builder.RegisterInstance(connection);
        }
    }
}
