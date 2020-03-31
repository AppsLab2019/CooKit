using CooKit.Services.Impl.SQLite;

namespace CooKit.Services.SQLite
{
    public interface ISQLitePictogramStoreBuilder : ISQLiteStoreBuilderBase<ISQLitePictogramStoreBuilder, IPictogramStore>
    {
        static ISQLitePictogramStoreBuilder CreateDefault() => 
            new SQLitePictogramStoreBuilder();
    }
}
