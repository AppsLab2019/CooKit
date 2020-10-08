using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace CooKit.Mobile.Resources
{
    public class AssemblyCustomResourceProvider : ICustomResourceProvider
    {
        public async Task<T> GetCustomResourceAsync<T>(string identifier)
        {
            var assembly = Assembly.GetCallingAssembly();
            var manifestName = GetManifestName(identifier);

            await using var stream = assembly.GetManifestResourceStream(manifestName);
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }

        private static string GetManifestName(string identifier)
        {
            return $"CooKit.Mobile.Resources.Custom.{identifier}.json";
        }
    }
}
