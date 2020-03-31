using CooKit.Services.Impl.SQLite;

namespace CooKit.Services.SQLite
{
    public interface ISQLiteIngredientStoreBuilder : ISQLiteStoreBuilderBase<ISQLiteIngredientStoreBuilder, IIngredientStore>
    {
        static ISQLiteIngredientStoreBuilder CreateDefault() => 
            new SQLiteIngredientStoreBuilder();
    }
}
