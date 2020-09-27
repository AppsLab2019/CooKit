using System.Threading.Tasks;

namespace CooKit.Mobile.Resources
{
    public interface ICustomResourceProvider
    {
        Task<T> GetCustomResourceAsync<T>(string identifier);
    }
}
