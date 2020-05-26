using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CooKit.Services.Extractors
{
    public sealed class ResourceExtractor : IResourceExtractor
    {
        public Task ExtractAsync(Assembly assembly, string resourceName, string destinationPath)
        {
            return InternalExtractAsync(assembly, resourceName, destinationPath, false);
        }

        public Task UnsafeExtractAsync(Assembly assembly, string resourceName, string destinationPath)
        {
            return InternalExtractAsync(assembly, resourceName, destinationPath, true);
        }

        public void UnsafeExtract(Assembly assembly, string resourceName, string destinationPath)
        {
            ValidateArguments(assembly, resourceName, destinationPath);

            using var resource = assembly.GetManifestResourceStream(resourceName);

            if (resource is null)
                ThrowResourceNotFound(assembly, resourceName);

            using var destination = File.Open(destinationPath, FileMode.Create, FileAccess.Write);
            resource!.CopyTo(destination);
        }

        private static async Task InternalExtractAsync(Assembly assembly, string resourceName, string destinationPath, 
            bool skipPermissionChecking)
        {
            ValidateArguments(assembly, resourceName, destinationPath);

            if (!skipPermissionChecking)
                await RequestPermissionsIfNeeded();

            await using var resource = assembly.GetManifestResourceStream(resourceName);

            if (resource is null)
                ThrowResourceNotFound(assembly, resourceName);

            await using var destination = File.Open(destinationPath, FileMode.Create, FileAccess.Write);
            await resource!.CopyToAsync(destination);
        }

        private static void ValidateArguments(Assembly assembly, string resourceName, string destinationPath)
        {
            if (assembly is null)
                throw new ArgumentNullException(nameof(assembly));

            if (string.IsNullOrEmpty(resourceName))
                throw new ArgumentException(nameof(resourceName));

            if (string.IsNullOrEmpty(destinationPath))
                throw new ArgumentException(nameof(destinationPath));
        }

        private static async Task RequestPermissionsIfNeeded()
        {
            if (await Permissions.CheckStatusAsync<Permissions.StorageWrite>() == PermissionStatus.Granted)
                return;

            if (await Permissions.RequestAsync<Permissions.StorageWrite>() == PermissionStatus.Granted)
                return;

            throw new AccessViolationException();
        }

        private static void ThrowResourceNotFound(Assembly assembly, string resourceName)
        {
            var assemblyName = assembly.FullName;
            throw new ArgumentException($"Resource {resourceName} was not found in {assemblyName} assembly!");
        }
    }
}
