using CooKit.Models;
using CooKit.Services.Impl.SQLite;

namespace CooKit.Services.SQLite
{
    public interface ISQLiteRecipeStoreBuilder : ISQLiteStoreBuilderBase<ISQLiteRecipeStoreBuilder, IRecipeStore>
    {
        IBuilderProperty<ISQLiteRecipeStoreBuilder, IIngredientStore> IngredientStore { get; }
        IBuilderProperty<ISQLiteRecipeStoreBuilder, IPictogramStore> PictogramStore { get; }
        IBuilderProperty<ISQLiteRecipeStoreBuilder, IStepStore> StepStore { get; }
        
        public static ISQLiteRecipeStoreBuilder CreateDefault() =>
            new SQLiteRecipeStoreBuilder();
    }
}
