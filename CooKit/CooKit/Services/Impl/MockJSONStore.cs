using System;
using System.Threading.Tasks;

namespace CooKit.Services.Impl
{
    public sealed class MockJSONStore : IJSONStore
    {
        public string GetJSON(JSONStoreType type, Guid guid) =>
            throw new NotImplementedException();

        public Task<string> GetJSONAsync(JSONStoreType type, Guid guid) =>
            throw new NotImplementedException();
    }
}
