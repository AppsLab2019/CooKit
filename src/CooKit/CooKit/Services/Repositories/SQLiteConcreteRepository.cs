using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Models;
using SQLite;

namespace CooKit.Services.Repositories
{
    public class SQLiteConcreteRepository<T> : IRepository<T> where T : IEntity, new()
    {
        protected readonly SQLiteAsyncConnection Connection;

        public SQLiteConcreteRepository(SQLiteAsyncConnection connection)
        {
            if (connection is null)
                throw new ArgumentNullException(nameof(connection));

            Connection = connection;
        }

        public async Task<IList<T>> GetAllEntries()
        {
            return await Connection.Table<T>().ToListAsync();
        }

        public Task<T> GetById(Guid id)
        {
            return Connection.Table<T>().FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<IList<T>> GetByIds(IEnumerable<Guid> ids)
        {
            if (ids is null)
                return null;

            var entities = await Connection.Table<T>().ToArrayAsync();
            var dictionary = entities.ToDictionary(entity => entity.Id);

            return ids.Select(id => SafeGetFromDictionary(id, dictionary)).ToList();
        }

        public Task Add(T entity) => 
            Connection.InsertAsync(entity);

        public Task Remove(T entity) => 
            Connection.DeleteAsync(entity);

        public Task Update(T entity) => 
            Connection.UpdateAsync(entity);

        private static T SafeGetFromDictionary(Guid id, IReadOnlyDictionary<Guid, T> dictionary)
        {
            return dictionary.ContainsKey(id) ? dictionary[id] : default;
        }
    }
}
