using CooKit.Models;
using CooKit.Services.Queries;

namespace CooKit.Services.Stores
{
    public interface IEntityStore<T> : IStore<T>, IQueryEntityById<T>, IQueryEntitiesByIds<T> where T : IEntity
    {
    }
}
