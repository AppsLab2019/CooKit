using CooKit.Models.Ingredients;
using SQLite;

namespace CooKit.Repositories.Ingredients
{
    public sealed class SQLiteIngredientTemplateRepository : SQLiteRepository<IIngredientTemplate, IngredientTemplate>, IIngredientTemplateRepository
    {
        public SQLiteIngredientTemplateRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }
    }
}
