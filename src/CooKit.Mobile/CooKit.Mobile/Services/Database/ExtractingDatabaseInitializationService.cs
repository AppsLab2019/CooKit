﻿using System.IO;
using System.Threading.Tasks;
using CooKit.Mobile.Providers.DatabasePath;
using CooKit.Mobile.Resources.Database;

namespace CooKit.Mobile.Services.Database
{
    public class ExtractingDatabaseInitializationService : IDatabaseInitializationService
    {
        private readonly IDatabaseExtractor _extractor;
        private readonly IDatabasePathProvider _pathProvider;

        public ExtractingDatabaseInitializationService(IDatabaseExtractor extractor, IDatabasePathProvider pathProvider)
        {
            _extractor = extractor;
            _pathProvider = pathProvider;
        }

        public Task InitializeDatabaseAsync()
        {
            var path = _pathProvider.GetDatabasePath();

            return !File.Exists(path)
                ? _extractor.ExtractDatabaseAsync(path)
                : Task.CompletedTask;
        }
    }
}
