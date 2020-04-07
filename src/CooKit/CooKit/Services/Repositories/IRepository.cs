using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Models;

namespace CooKit.Services.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task<IList<T>> GetAllEntries();

        Task<T> GetById(Guid id);
        Task<IList<T>> GetByIds(IEnumerable<Guid> ids);
    }
}
