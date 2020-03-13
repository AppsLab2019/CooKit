using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using CooKit.Models;

namespace CooKit.Services
{
    public interface IStoreBase<TStorable, TStorableBuilder> : INotifyPropertyChanged
        where TStorable : IStorable
    {
        IReadOnlyList<TStorable> LoadedObjects { get; }

        TStorableBuilder CreateBuilder();

        TStorable LoadNext();
        Task<TStorable> LoadNextAsync();

        TStorable Load(Guid id);
        Task<TStorable> LoadAsync(Guid id);

        void Add(TStorableBuilder builder);
        Task AddAsync(TStorableBuilder builder);

        bool Remove(Guid id);
        Task<bool> RemoveAsync(Guid id);
    }
}
