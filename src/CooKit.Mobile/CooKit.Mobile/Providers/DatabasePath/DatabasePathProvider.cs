using System;
using System.IO;

namespace CooKit.Mobile.Providers.DatabasePath
{
    public class DatabasePathProvider : IDatabasePathProvider
    {
        private const string DatabaseName = "CooKit.db3";

        public string GetDatabasePath()
        {
            var dataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(dataPath, DatabaseName);
        }
    }
}
