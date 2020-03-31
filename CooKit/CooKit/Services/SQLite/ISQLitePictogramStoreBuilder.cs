using CooKit.Models;
using CooKit.Services.Impl.SQLite;
using SQLite;

namespace CooKit.Services.SQLite
{
    public interface ISQLitePictogramStoreBuilder : IAsyncBuilder<IPictogramStore>
    {
        IBuilderProperty<ISQLitePictogramStoreBuilder, SQLiteAsyncConnection> Connection { get; }
        IBuilderProperty<ISQLitePictogramStoreBuilder, IImageStore> ImageStore { get; }

        static ISQLitePictogramStoreBuilder CreateDefault() => 
            new SQLitePictogramStoreBuilder();
    }
}
