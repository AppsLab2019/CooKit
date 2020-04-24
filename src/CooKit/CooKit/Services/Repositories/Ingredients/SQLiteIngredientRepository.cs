using System.Threading.Tasks;
using AutoMapper;
using CooKit.Models.Ingredients;
using CooKit.Services.Queries;

namespace CooKit.Services.Repositories.Ingredients
{
    public sealed class SQLiteIngredientRepository : MappingRepository<IIngredient, SQLiteIngredientDto>, IIngredientRepository
    {
        private readonly IQueryEntityById<IIngredientTemplate> _ingredientTemplateQuery;

        public SQLiteIngredientRepository(IMapper mapper, ISQLiteIngredientDtoRepository repository,
            IQueryEntityById<IIngredientTemplate> ingredientTemplateQuery) : base(mapper, repository)
        {
            if (ingredientTemplateQuery is null)
                throw new System.ArgumentNullException(nameof(ingredientTemplateQuery));

            _ingredientTemplateQuery = ingredientTemplateQuery;
        }

        protected override async Task<IIngredient> MapDtoToEntity(SQLiteIngredientDto dto)
        {
            var entity = await base.MapDtoToEntity(dto);
            entity.Template = await _ingredientTemplateQuery.GetById(dto.TemplateId);
            return entity;
        }

        protected override async Task<SQLiteIngredientDto> MapEntityToDto(IIngredient entity)
        {
            var dto = await base.MapEntityToDto(entity);
            dto.TemplateId = entity.Template.Id;
            return dto;
        }
    }
}
