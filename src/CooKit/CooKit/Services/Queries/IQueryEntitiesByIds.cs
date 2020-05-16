using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Models;

namespace CooKit.Services.Queries
{
    public interface IQueryEntitiesByIds<T> where T : IEntity
    {
        Task<IList<T>> GetByIds(IEnumerable<Guid> ids);
    }
}
