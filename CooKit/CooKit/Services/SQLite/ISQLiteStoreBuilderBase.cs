using CooKit.Models;
using SQLite;

namespace CooKit.Services.SQLite
{
    public interface ISQLiteStoreBuilderBase<out TBuilder, T> : IAsyncBuilder<T>
    {
        IBuilderProperty<TBuilder, SQLiteAsyncConnection> Connection { get; }
        IBuilderProperty<TBuilder, IImageStore> ImageStore { get; }
    }
}
