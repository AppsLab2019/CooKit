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

        Task<TStorable> LoadAsync(Guid id);
        Task<TStorable> LoadNextAsync();

        Task AddAsync(TStorableBuilder builder);
        Task<bool> RemoveAsync(Guid id);
    }
}
