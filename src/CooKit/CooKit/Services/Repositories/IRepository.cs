using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Models;
using CooKit.Services.Queries;

namespace CooKit.Services.Repositories
{
    public interface IRepository<T> : IQueryEntityById<T> where T : IEntity
    {
        Task<IList<T>> GetAllEntries();

        Task<IList<T>> GetByIds(IEnumerable<Guid> ids);

        Task Add(T entity);
        Task Remove(T entity);
        Task Update(T entity);
    }
}
