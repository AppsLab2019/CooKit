using CooKit.Models.Recipes;
using SQLite;

namespace CooKit.Services.Repositories.Recipes
{
    public sealed class SQLiteRecipeDtoRepository : SQLiteConcreteRepository<SQLiteRecipeDto>, ISQLiteRecipeDtoRepository
    {
        public SQLiteRecipeDtoRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }
    }
}
