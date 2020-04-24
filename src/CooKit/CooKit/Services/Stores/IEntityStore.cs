using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Models;
using CooKit.Services.Queries;

namespace CooKit.Services.Stores
{
    public interface IEntityStore<T> : IStore<T>, IQueryEntityById<T> where T : IEntity
    {
        Task<IList<T>> GetByIds(IEnumerable<Guid> ids);
    }
}
