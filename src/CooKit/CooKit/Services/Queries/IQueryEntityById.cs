using System;
using System.Threading.Tasks;
using CooKit.Models;

namespace CooKit.Services.Queries
{
    public interface IQueryEntityById<T> where T : IEntity
    {
        Task<T> GetById(Guid id);
    }
}
