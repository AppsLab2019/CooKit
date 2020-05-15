using System.Reflection;
using System.Threading.Tasks;

namespace CooKit.Services.Extractors
{
    public interface IResourceExtractor
    {
        void UnsafeExtract(Assembly assembly, string resourceName, string destinationPath);

        Task ExtractAsync(Assembly assembly, string resourceName, string destinationPath);
        Task UnsafeExtractAsync(Assembly assembly, string resourceName, string destinationPath);
    }
}
