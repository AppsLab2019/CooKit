using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Models;
using SQLite;

namespace CooKit.Repositories
{
    public class SQLiteRepository<T, TImpl> : IRepository<T> where T : IEntity where TImpl : T, new()
    {
        private readonly SQLiteAsyncConnection _connection;

        public SQLiteRepository(SQLiteAsyncConnection connection)
        {
            if (connection is null)
                throw new ArgumentNullException(nameof(connection));

            _connection = connection;
        }

        public async Task<IList<T>> GetAllEntries()
        {
            var entries = await _connection.Table<TImpl>().ToArrayAsync();
            return entries.Cast<T>().ToList();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _connection.Table<TImpl>().FirstAsync(entry => entry.Id == id);
        }

        public async Task<IList<T>> GetByIds(IEnumerable<Guid> ids)
        {
            if (ids is null)
                return null;

            var entries = await _connection.Table<TImpl>().ToArrayAsync();
            var dictionary = entries.ToDictionary(entry => entry.Id);

            var selectedEntries = ids.Select(id => SafeGetFromDictionary(dictionary, id));
            return selectedEntries.Cast<T>().ToList();
        }

        public Task Add(T entity)
        {
            var concrete = CastToConcreteOrThrow(entity);
            return _connection.InsertAsync(concrete);
        }

        public Task Remove(T entity)
        {
            var concrete = CastToConcreteOrThrow(entity);
            return _connection.DeleteAsync(concrete);
        }

        public Task Update(T entity)
        {
            var concrete = CastToConcreteOrThrow(entity);
            return _connection.UpdateAsync(concrete);
        }

        private static TImpl SafeGetFromDictionary(IDictionary<Guid, TImpl> dict, Guid id)
        {
            return dict.ContainsKey(id) ? dict[id] : default;
        }

        private TImpl CastToConcreteOrThrow(T entity)
        {
            if (entity is TImpl concrete)
                return concrete;

            throw new NotSupportedException();
        }
    }
}
