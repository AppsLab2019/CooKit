using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CooKit.Models;
using SQLite;

namespace CooKit.Services.Impl.SQLite
{
    internal abstract class SQLiteStoreBase<TStorable, TStorableBuilder, TStorableInternal, TStorableInfo> : 
        IStoreBase<TStorable, TStorableBuilder>
        where TStorable : IStorable
        where TStorableInternal : ISQLiteStorable<TStorableInfo>, TStorable
        where TStorableInfo : new()
    {
        protected internal readonly SQLiteAsyncConnection Connection;

        private List<TStorable> _objects;
        private Dictionary<Guid, TStorableInternal> _idToObject;

        public IReadOnlyList<TStorable> LoadedObjects => _objects;
        public event PropertyChangedEventHandler PropertyChanged;

        protected internal SQLiteStoreBase(SQLiteAsyncConnection connection) => 
            Connection = connection;

        public abstract TStorableBuilder CreateBuilder();

        public Task<TStorable> LoadNextAsync() =>
            Task.FromResult(default(TStorable));

        private TStorable Load(Guid id) =>
            _idToObject.ContainsKey(id) ? _idToObject[id] : default;

        public async Task<TStorable> LoadAsync(Guid id) =>
            await Task.Run(() => Load(id));

        public async Task AddAsync(TStorableBuilder builder)
        {
            var obj = await CreateObjectFromBuilder(builder);

            await AddObjectToDatabase(obj);

            _objects.Add(obj);
            _idToObject.Add(obj.Id, obj);

            RaisePropertyChanged(nameof(LoadedObjects));
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            if (!_idToObject.TryGetValue(id, out var obj))
                return false;

            await RemoveObjectFromDatabase(obj);

            _objects.Remove(obj);
            _idToObject.Remove(obj.Id);

            RaisePropertyChanged(nameof(LoadedObjects));
            return true;
        }

        internal async Task InitAsync()
        {
            await Connection.CreateTableAsync<TStorableInfo>();

            await PreInitAsync();

            var infos = await Connection
                .Table<TStorableInfo>()
                .ToArrayAsync();

            var objectTasks = infos
                .Select(CreateObjectFromInfo)
                .ToList();

            var objects = new List<TStorableInternal>();

            foreach (var objectTask in objectTasks)
                objects.Add(await objectTask);

            _objects = objects
                .Cast<TStorable>()
                .ToList();

            _idToObject = objects.ToDictionary(obj => obj.Id);

            await PostInitAsync();
        }

        protected internal virtual Task AddObjectToDatabase(TStorableInternal storable) =>
            Connection.InsertAsync(storable.InternalInfo);

        protected internal virtual Task RemoveObjectFromDatabase(TStorableInternal storable) =>
            Connection.DeleteAsync(storable.InternalInfo);

        protected internal virtual Task PreInitAsync() => Task.CompletedTask;
        protected internal virtual Task PostInitAsync() => Task.CompletedTask;

        protected internal abstract Task<TStorableInternal> CreateObjectFromBuilder(TStorableBuilder builder);
        protected internal abstract Task<TStorableInternal> CreateObjectFromInfo(TStorableInfo info);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal void RaisePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
