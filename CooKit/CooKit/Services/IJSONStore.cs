using System;
using System.Threading.Tasks;

namespace CooKit.Services
{
    public interface IJSONStore
    {
        string GetJSON(JSONStoreType type, Guid guid);
        Task<string> GetJSONAsync(JSONStoreType type, Guid guid);
    }
}
