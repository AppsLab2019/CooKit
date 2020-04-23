using CooKit.Models.Ingredients;
using SQLite;

namespace CooKit.Services.Repositories.Ingredients
{
    public sealed class SQLiteIngredientDtoRepository : SQLiteConcreteRepository<SQLiteIngredientDto>, ISQLiteIngredientDtoRepository
    {
        public SQLiteIngredientDtoRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }
    }
}
