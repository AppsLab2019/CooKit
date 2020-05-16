using System;
using System.IO;
using System.Reflection;
using CooKit.Services.Extractors;
using SQLite;

namespace CooKit.Services.Database
{
    public sealed class SQLiteConnectionFactory : ISQLiteConnectionFactory
    {
        private readonly IResourceExtractor _extractor;

        public SQLiteConnectionFactory(IResourceExtractor extractor)
        {
            if (extractor is null)
                throw new ArgumentNullException(nameof(extractor));

            _extractor = extractor;
        }

        public SQLiteAsyncConnection CreateConnection()
        {
            var path = GetPath();
            ExtractDb(path);
            return new SQLiteAsyncConnection(path);
        }

        private static string GetPath()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(folder, "CooKit.db3");
        }

        private void ExtractDb(string path)
        {
            if (File.Exists(path))
                return;

            var assembly = Assembly.GetExecutingAssembly();
            _extractor.UnsafeExtract(assembly, "CooKit.CooKit.db3", path);
        }
    }
}
