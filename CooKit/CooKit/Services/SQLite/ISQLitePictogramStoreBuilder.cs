using CooKit.Services.Impl.SQLite;

namespace CooKit.Services.SQLite
{
    public interface ISQLitePictogramStoreBuilder : ISQLiteStoreBuilderBase<ISQLitePictogramStoreBuilder, IPictogramStore>
    {
        public static ISQLitePictogramStoreBuilder CreateDefault() => 
            new SQLitePictogramStoreBuilder();
    }
}
