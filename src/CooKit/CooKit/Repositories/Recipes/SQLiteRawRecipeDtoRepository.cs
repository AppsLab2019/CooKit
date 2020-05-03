using CooKit.Models.Recipes;
using SQLite;

namespace CooKit.Repositories.Recipes
{
    public sealed class SQLiteRawRecipeDtoRepository : SQLiteConcreteRepository<SQLiteRawRecipeDto>, ISQLiteRawRecipeDtoRepository
    {
        public SQLiteRawRecipeDtoRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }
    }
}
