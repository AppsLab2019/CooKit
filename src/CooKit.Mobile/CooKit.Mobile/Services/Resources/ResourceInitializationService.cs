using System.IO;
using System.Threading.Tasks;
using CooKit.Mobile.Providers.ResourcePath;

namespace CooKit.Mobile.Services.Resources
{
    public class ResourceInitializationService : IResourceInitializationService
    {
        private readonly IResourcePathProvider _pathProvider;

        public ResourceInitializationService(IResourcePathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public Task InitializeResourcesAsync()
        {
            var path = _pathProvider.GetResourceFolderPath();
            return Task.Run(() => Directory.CreateDirectory(path));

            // TODO: extract needed resources here
        }
    }
}
