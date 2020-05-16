using System;
using System.Reflection;
using System.Threading.Tasks;
using SQLite;

namespace CooKit.Services.Database
{
    public sealed class SQLiteInitialization : ISQLiteInitialization
    {
        private readonly SQLiteAsyncConnection _connection;

        public SQLiteInitialization(SQLiteAsyncConnection connection)
        {
            if (connection is null)
                throw new ArgumentNullException(nameof(connection));

            _connection = connection;
        }

        public async Task InitializeAsync()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                var tableAttribute = type.GetCustomAttribute<TableAttribute>();

                if (tableAttribute is null)
                    continue;

                await _connection.CreateTableAsync(type);
            }
        }
    }
}
