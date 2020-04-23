using AutoMapper;
using CooKit.Models.Recipes;

namespace CooKit.Services.Repositories.Recipes
{
    public sealed class SQLiteRecipeRepository : MappingRepository<IRecipe, SQLiteRecipeDto>, IRecipeRepository
    {
        public SQLiteRecipeRepository(IMapper mapper, ISQLiteRecipeDtoRepository repository) : base(mapper, repository)
        {
        }
    }
}
