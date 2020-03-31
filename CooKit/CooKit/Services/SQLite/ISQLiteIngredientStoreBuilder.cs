using CooKit.Services.Impl.SQLite;

namespace CooKit.Services.SQLite
{
    public interface ISQLiteIngredientStoreBuilder : ISQLiteStoreBuilderBase<ISQLiteIngredientStoreBuilder, IIngredientStore>
    {
        public static ISQLiteIngredientStoreBuilder CreateDefault() => 
            new SQLiteIngredientStoreBuilder();
    }
}
