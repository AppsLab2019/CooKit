using CooKit.Services.Impl.SQLite;

namespace CooKit.Services.SQLite
{
    public interface ISQLiteStepStoreBuilder : ISQLiteStoreBuilderBase<ISQLiteStepStoreBuilder, IRecipeStepStore>
    {
        public static ISQLiteStepStoreBuilder CreateDefault() =>
            new SQLiteStepStoreBuilder();
    }
}
