using System.Collections.Generic;
using System.Threading.Tasks;

namespace CooKit.Services.Stores
{
    public interface IStore<T>
    {
        Task<IList<T>> GetAll();
        Task Update(T item);

        Task Refresh();
    }
}
