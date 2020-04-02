using CooKit.Services.Impl.SQLite;

namespace CooKit.Services.SQLite
{
    public interface ISQLiteStepStoreBuilder : ISQLiteStoreBuilderBase<ISQLiteStepStoreBuilder, IStepStore>
    {
        public static ISQLiteStepStoreBuilder CreateDefault() =>
            new SQLiteStepStoreBuilder();
    }
}
