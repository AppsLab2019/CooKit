using System.Reflection;
using System.Threading.Tasks;

namespace CooKit.Mobile.Resources
{
    public interface IResourceExtractor
    {
        Task ExtractResourceAsync(string name, string destinationPath);
        Task ExtractResourceAsync(string name, string destinationPath, Assembly assembly);
    }
}
