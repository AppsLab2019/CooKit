using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CooKit.Models;

namespace CooKit.Services
{
    public interface IStoreBase<TStorable, TStorableBuilder>
        where TStorable : IStorable
    {
        ReadOnlyObservableCollection<TStorable> LoadedObjects { get; }

        TStorableBuilder CreateBuilder();

        Task<TStorable> LoadAsync(Guid id);
        Task<TStorable> LoadNextAsync();

        Task AddAsync(TStorableBuilder builder);
        Task<bool> RemoveAsync(Guid id);
    }
}
