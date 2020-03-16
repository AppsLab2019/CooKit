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
        private protected readonly SQLiteAsyncConnection Connection;

        private List<TStorable> _objects;
        private Dictionary<Guid, TStorableInternal> _idToObject;

        public IReadOnlyList<TStorable> LoadedObjects => _objects;
        public event PropertyChangedEventHandler PropertyChanged;

        private protected SQLiteStoreBase(SQLiteAsyncConnection connection) => 
            Connection = connection;

        public abstract TStorableBuilder CreateBuilder();

        public virtual Task<TStorable> LoadNextAsync() =>
            Task.FromResult(default(TStorable));

        public virtual Task<TStorable> LoadAsync(Guid id) =>
            Task.FromResult((TStorable) (_idToObject.ContainsKey(id) ? _idToObject[id] : default));

        public virtual async Task AddAsync(TStorableBuilder builder)
        {
            var info = await CreateInfoFromBuilder(builder);
            var obj = await CreateObjectFromInfo(info);

            await AddObjectToDatabase(obj);

            _objects.Add(obj);
            _idToObject.Add(obj.Id, obj);

            RaisePropertyChanged(nameof(LoadedObjects));
        }

        public virtual async Task<bool> RemoveAsync(Guid id)
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
                .ToArray();

            await Task.WhenAll(objectTasks);

            var objects = objectTasks
                .Select(task => task.Result)
                .ToArray();

            _objects = objects
                .Cast<TStorable>()
                .ToList();

            _idToObject = objects.ToDictionary(obj => obj.Id);

            await PostInitAsync();
        }

        private protected virtual Task AddObjectToDatabase(TStorableInternal storable) =>
            Connection.InsertAsync(storable.InternalInfo);

        private protected virtual Task RemoveObjectFromDatabase(TStorableInternal storable) =>
            Connection.DeleteAsync(storable.InternalInfo);

        private protected virtual Task PreInitAsync() => Task.CompletedTask;
        private protected virtual Task PostInitAsync() => Task.CompletedTask;

        private protected abstract Task<TStorableInfo> CreateInfoFromBuilder(TStorableBuilder builder);
        private protected abstract Task<TStorableInternal> CreateObjectFromInfo(TStorableInfo info);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private protected void RaisePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
