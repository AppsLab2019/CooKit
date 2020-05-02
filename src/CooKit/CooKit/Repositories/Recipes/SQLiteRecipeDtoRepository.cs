using CooKit.Models.Recipes;
using SQLite;

namespace CooKit.Repositories.Recipes
{
    public sealed class SQLiteRecipeDtoRepository : SQLiteConcreteRepository<SQLiteRecipeDto>, ISQLiteRecipeDtoRepository
    {
        public SQLiteRecipeDtoRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }
    }
}
