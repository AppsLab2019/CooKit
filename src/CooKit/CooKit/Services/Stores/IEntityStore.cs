using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Models;

namespace CooKit.Services.Stores
{
    public interface IEntityStore<T> : IStore<T> where T : IEntity
    {
        Task<T> GetById(Guid id);
        Task<IList<T>> GetByIds(IEnumerable<Guid> ids);
    }
}
