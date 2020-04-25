using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using CooKit.Extensions;
using CooKit.ViewModels;
using SQLite;

namespace CooKit.Strategies.Initialization.App
{
    public sealed class AppInitializationStrategy : IAppInitializationStrategy
    {
        public async Task InitializeApp(IContainer container)
        {
            ViewModelLocator.Initialize(container);

            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();

            await InitializeDatabase(container, types);
        }

        private static async Task InitializeDatabase(IContainer container, IEnumerable<Type> types)
        {
            var lazyConnection = container.LazyResolve<SQLiteAsyncConnection>();

            foreach (var type in types)
            {
                var tableAttribute = type.GetCustomAttribute<TableAttribute>();

                if (tableAttribute is null)
                    continue;

                await lazyConnection.Value.CreateTableAsync(type);
            }
        }
    }
}
