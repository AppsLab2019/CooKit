using CooKit.Models;
using SQLite;

namespace CooKit.Services.Repositories.Ingredients
{
    public sealed class SQLiteIngredientRepository : GenericSQLiteRepository<Ingredient>, IIngredientRepository
    {
        public SQLiteIngredientRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }
    }
}
