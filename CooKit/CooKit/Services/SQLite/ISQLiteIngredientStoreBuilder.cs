using CooKit.Models;
using CooKit.Services.Impl.SQLite;
using SQLite;

namespace CooKit.Services.SQLite
{
    public interface ISQLiteIngredientStoreBuilder : IAsyncBuilder<IIngredientStore>
    {
        IBuilderProperty<ISQLiteIngredientStoreBuilder, SQLiteAsyncConnection> Connection { get; }
        IBuilderProperty<ISQLiteIngredientStoreBuilder, IImageStore> ImageStore { get; }

        static ISQLiteIngredientStoreBuilder CreateDefault() => 
            new SQLiteIngredientStoreBuilder();
    }
}
