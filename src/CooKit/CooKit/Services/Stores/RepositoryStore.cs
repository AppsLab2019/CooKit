using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Models;
using CooKit.Services.Repositories;

namespace CooKit.Services.Stores
{
    public class RepositoryStore<T> : IEntityStore<T> where T : IEntity
    {
        private readonly IRepository<T> _repository;
        private IDictionary<Guid, T> _entries;
        public RepositoryStore(IRepository<T> repository)
        {
            if (repository is null)
                throw new ArgumentNullException(nameof(repository));

            _repository = repository;
        }

        public async Task Refresh()
        {
            var entries = await _repository.GetAllEntries();
            _entries = entries.ToDictionary(entry => entry.Id);
        }
        public async Task<IList<T>> GetAll()
        {
            await RefreshIfNeeded();
            return _entries.Values.ToList();
        }

        public async Task<T> GetById(Guid id)
        {
            await RefreshIfNeeded();

            if (_entries.ContainsKey(id))
                return _entries[id];

            return await _repository.GetById(id);
        }

        public async Task<IList<T>> GetByIds(IEnumerable<Guid> ids)
        {
            await RefreshIfNeeded();

            if (ids is null)
                return null;

            var missing = new List<Guid>();
            var entries = new List<T>();

            foreach (var id in ids)
            {
                if (_entries.ContainsKey(id))
                {
                    entries.Add(_entries[id]);
                    continue;
                }

                missing.Add(id);
            }

            var retrieved = await _repository.GetByIds(missing);
            entries.AddRange(retrieved);
            return entries;
        }

        public Task Update(T item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            if (item.Id != default)
                return _repository.Update(item);

            item.Id = Guid.NewGuid();
            return _repository.Add(item);
        }

        public Task RefreshIfNeeded()
        {
            if (_entries is null)
                return Refresh();

            return Task.CompletedTask;
        }
    }
}
