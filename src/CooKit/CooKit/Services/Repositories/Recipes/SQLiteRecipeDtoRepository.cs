using CooKit.Models;
using SQLite;

namespace CooKit.Services.Repositories.Recipes
{
    public sealed class SQLiteRecipeDtoRepository : GenericSQLiteRepository<SQLiteRecipeDto>, ISQLiteRecipeDtoRepository
    {
        public SQLiteRecipeDtoRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }
    }
}
