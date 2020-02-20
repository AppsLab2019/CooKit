using System;
using System.Threading.Tasks;

namespace CooKit.Services
{
    public interface IJsonStore
    {
        string GetJson(JsonStoreType type, Guid guid);
        Task<string> GetJsonAsync(JsonStoreType type, Guid guid);
    }
}
