using CooKit.Models.Ingredients;
using SQLite;

namespace CooKit.Services.Repositories.Ingredients
{
    public sealed class SQLiteIngredientTemplateRepository : SQLiteRepository<IIngredientTemplate, IngredientTemplate>, IIngredientTemplateRepository
    {
        public SQLiteIngredientTemplateRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }
    }
}
