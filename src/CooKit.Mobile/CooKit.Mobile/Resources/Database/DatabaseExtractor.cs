using System.Threading.Tasks;

namespace CooKit.Mobile.Resources.Database
{
    public class DatabaseExtractor : IDatabaseExtractor
    {
        private readonly IResourceExtractor _extractor;
        private const string ResourceName = "CooKit.Mobile.Resources.Database.CooKit.db3";

        public DatabaseExtractor(IResourceExtractor extractor)
        {
            _extractor = extractor;
        }

        public Task ExtractDatabaseAsync(string destinationPath)
        {
            return _extractor.ExtractResourceAsync(ResourceName, destinationPath);
        }
    }
}
