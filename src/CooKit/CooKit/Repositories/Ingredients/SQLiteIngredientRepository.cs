using System;
using System.Threading.Tasks;
using CooKit.Models.Ingredients;
using CooKit.Services.Queries;

namespace CooKit.Repositories.Ingredients
{
    public sealed class SQLiteIngredientRepository : MappingRepository<IIngredient, IIngredientDto>, IIngredientRepository
    {
        private readonly IQueryEntityById<IIngredientTemplate> _ingredientTemplateQuery;

        public SQLiteIngredientRepository(IIngredientDtoRepository repository, 
            IQueryEntityById<IIngredientTemplate> ingredientTemplateQuery) : base(repository)
        {
            if (ingredientTemplateQuery is null)
                throw new ArgumentNullException(nameof(ingredientTemplateQuery));

            _ingredientTemplateQuery = ingredientTemplateQuery;
        }

        protected override async Task<IIngredient> MapDtoToEntity(IIngredientDto dto)
        {
            return new Ingredient
            {
                Id = dto.Id,
                Note = dto.Note,
                Quantity = dto.Quantity,
                Template = await _ingredientTemplateQuery.GetById(dto.TemplateId)
            };
        }

        protected override Task<IIngredientDto> MapEntityToDto(IIngredient entity)
        {
            IIngredientDto dto = new IngredientDto
            {
                Id = entity.Id,
                Note = entity.Note,
                Quantity = entity.Quantity,
                TemplateId = entity.Template.Id
            };

            return Task.FromResult(dto);
        }
    }
}
