using CooKit.Services.Impl.SQLite;

namespace CooKit.Services.SQLite
{
    public interface ISQLiteStepStoreBuilder : ISQLiteStoreBuilderBase<ISQLiteStepStoreBuilder, IRecipeStepStore>
    {
        ISQLiteStepStoreBuilder CreateDefault() =>
            new SQLiteStepStoreBuilder();
    }
}
