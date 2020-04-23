using AutoMapper;
using CooKit.Models.Ingredients;

namespace CooKit.Services.Repositories.Ingredients
{
    public sealed class SQLiteIngredientRepository : MappingRepository<IIngredient, SQLiteIngredientDto>, IIngredientRepository
    {
        public SQLiteIngredientRepository(IMapper mapper, ISQLiteIngredientDtoRepository repository) : base(mapper, repository)
        {
        }
    }
}
