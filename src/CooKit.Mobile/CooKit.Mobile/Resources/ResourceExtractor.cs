using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using CooKit.Mobile.Exceptions.Resources;
using Microsoft.Extensions.Logging;

namespace CooKit.Mobile.Resources
{
    public class ResourceExtractor : IResourceExtractor
    {
        private readonly ILogger<ResourceExtractor> _logger;

        public ResourceExtractor(ILogger<ResourceExtractor> logger)
        {
            _logger = logger;
        }

        public Task ExtractResourceAsync(string name, string destinationPath)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();
            return ExtractResourceAsync(name, destinationPath, thisAssembly);
        }

        // TODO: refactor the file opening logic out of this method / class
        public async Task ExtractResourceAsync(string name, string destinationPath, Assembly assembly)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (destinationPath == null)
                throw new ArgumentNullException(nameof(destinationPath));

            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            await using var resourceStream = OpenResourceStream(name, assembly);
            await using var destinationStream = File.Open(destinationPath, FileMode.Create, FileAccess.Write);
            await resourceStream.CopyToAsync(destinationStream);

            _logger.LogDebug($"Successfully extracted resource {name} into {destinationPath}!");
        }

        private static Stream OpenResourceStream(string name, Assembly assembly)
        {
            var resourceStream = assembly.GetManifestResourceStream(name);

            if (resourceStream == null)
                throw new ResourceNotFoundException(name);

            return resourceStream;
        }
    }
}
