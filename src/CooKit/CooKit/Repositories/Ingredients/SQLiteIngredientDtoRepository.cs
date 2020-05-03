using CooKit.Models.Ingredients;
using SQLite;

namespace CooKit.Repositories.Ingredients
{
    public sealed class SQLiteIngredientDtoRepository : SQLiteRepository<IIngredientDto, IngredientDto>, IIngredientDtoRepository
    {
        public SQLiteIngredientDtoRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }
    }
}
