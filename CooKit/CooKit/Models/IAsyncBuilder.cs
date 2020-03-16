using System.Threading.Tasks;

namespace CooKit.Models
{
    public interface IAsyncBuilder<T>
    {
        Task<T> BuildAsync();
    }
}
