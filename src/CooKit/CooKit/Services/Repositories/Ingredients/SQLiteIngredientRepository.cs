using System.Threading.Tasks;
using AutoMapper;
using CooKit.Models.Ingredients;

namespace CooKit.Services.Repositories.Ingredients
{
    public sealed class SQLiteIngredientRepository : MappingRepository<IIngredient, SQLiteIngredientDto>, IIngredientRepository
    {
        private readonly IIngredientTemplateRepository _templateRepository;

        public SQLiteIngredientRepository(IMapper mapper, ISQLiteIngredientDtoRepository repository,
            IIngredientTemplateRepository templateRepository) : base(mapper, repository)
        {
            if (templateRepository is null)
                throw new System.ArgumentNullException(nameof(templateRepository));

            _templateRepository = templateRepository;
        }

        protected override async Task<IIngredient> MapDtoToEntity(SQLiteIngredientDto dto)
        {
            var entity = await base.MapDtoToEntity(dto);
            entity.Template = await _templateRepository.GetById(dto.TemplateId);
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
