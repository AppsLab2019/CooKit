using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Models;
using CooKit.Services.Queries;

namespace CooKit.Repositories
{
    public interface IRepository<T> : IQueryEntityById<T>, IQueryEntitiesByIds<T> where T : IEntity
    {
        Task<IList<T>> GetAllEntries();

        Task Add(T entity);
        Task Remove(T entity);
        Task Update(T entity);
    }
}
