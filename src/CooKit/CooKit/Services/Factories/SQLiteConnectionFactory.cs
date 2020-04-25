using System;
using System.IO;
using SQLite;

namespace CooKit.Services.Factories
{
    public sealed class SQLiteConnectionFactory : ISQLiteConnectionFactory
    {
        public SQLiteAsyncConnection CreateConnection()
        {
            var path = GetPath();
            return new SQLiteAsyncConnection(path);
        }

        private static string GetPath()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(folder, "CooKit.db3");
        }
    }
}
