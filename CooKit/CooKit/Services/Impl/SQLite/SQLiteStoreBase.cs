using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Models;
using SQLite;
using Xamarin.Forms;

namespace CooKit.Services.Impl.SQLite
{
    internal abstract class SQLiteStoreBase<T, TBuilder, TInternal> : IStoreBase<T, TBuilder> where T : IStorable where TInternal : new()
    {
        public ReadOnlyObservableCollection<T> LoadedObjects { get; private set; }

        private protected readonly SQLiteAsyncConnection Connection;
        private protected readonly IImageStore ImageStore;

        private protected ObservableCollection<T> LoadedObjectsInternal;
        private protected IDictionary<Guid, T> IdToObject;

        private protected SQLiteStoreBase(SQLiteAsyncConnection connection, IImageStore imageStore)
        {
            Connection = connection;
            ImageStore = imageStore;
        }

        public abstract TBuilder CreateBuilder();

        public Task<T> LoadAsync(Guid id) =>
            Task.FromResult(IdToObject.TryGetValue(id, out var obj) ? obj : default);

        public Task<T> LoadNextAsync() => 
            Task.FromResult(default(T));

        public async Task AddAsync(TBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            var info = await BuilderToInternalInfo(builder);
            await Connection.InsertAsync(info);

            var obj = await InternalInfoToObject(info);

            IdToObject[obj.Id] = obj;
            LoadedObjectsInternal.Add(obj);
        }

        public Task<bool> RemoveAsync(Guid id)
        {
            if (!IdToObject.ContainsKey(id))
                return Task.FromResult(false);

            IdToObject.Remove(id, out var obj);
            LoadedObjectsInternal.Remove(obj);

            return Connection.DeleteAsync(obj.Id).ContinueWith(_ => true);
        }

        internal async Task InitAsync()
        {
            await PreInitAsync();

            var infos = await Connection
                .Table<TInternal>()
                .ToArrayAsync();

            var objectTasks = infos
                .Select(InternalInfoToObject)
                .ToArray();

            await Task.WhenAll(objectTasks);

            LoadedObjectsInternal = new ObservableCollection<T>(objectTasks.Select(task => task.Result));
            LoadedObjects = new ReadOnlyObservableCollection<T>(LoadedObjectsInternal);

            IdToObject = LoadedObjectsInternal.ToDictionary(obj => obj.Id);

            await PostInitAsync();
        }

        private protected virtual Task PreInitAsync() =>
            Connection.CreateTableAsync<TInternal>();

        private protected virtual Task PostInitAsync() =>
            Task.CompletedTask;

        private protected abstract Task<T> InternalInfoToObject(TInternal info);
        private protected abstract Task<TInternal> BuilderToInternalInfo(TBuilder builder);

        private protected Task<ImageSource> SafeImageLoadAsync(string loader, string source, ImageSource defaultImage = null)
        {
            if (loader is null || source is null)
                return Task.FromResult(defaultImage);

            return ImageStore.LoadImageAsync(loader, source);
        }
    }
}
