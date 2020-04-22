using CooKit.Models.Ingredients;
using SQLite;

namespace CooKit.Services.Repositories.Ingredients
{
    public sealed class SQLiteIngredientRepository : SQLiteRepository<IIngredient, Ingredient>, IIngredientRepository
    {
        public SQLiteIngredientRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }
    }
}
