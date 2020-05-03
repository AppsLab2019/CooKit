using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Models;
using CooKit.Models.Ingredients;
using CooKit.Models.Pictograms;
using CooKit.Models.Recipes;
using CooKit.Services.Queries;

namespace CooKit.Repositories.Recipes
{
    public sealed class SQLiteRecipeRepository : MappingRepository<IRecipe, SQLiteRawRecipeDto>, IRecipeRepository
    {
        private readonly IQueryEntitiesByIds<IIngredient> _ingredientsQuery;
        private readonly IQueryEntitiesByIds<IPictogram> _pictogramsQuery;

        public SQLiteRecipeRepository(ISQLiteRawRecipeDtoRepository repository,
            IQueryEntitiesByIds<IIngredient> ingredientsQuery, 
            IQueryEntitiesByIds<IPictogram> pictogramsQuery) : base(repository)
        {
            if (ingredientsQuery is null)
                throw new ArgumentNullException(nameof(ingredientsQuery));

            if (pictogramsQuery is null)
                throw new ArgumentNullException(nameof(pictogramsQuery));

            _ingredientsQuery = ingredientsQuery;
            _pictogramsQuery = pictogramsQuery;
        }

        protected override async Task<IRecipe> MapDtoToEntity(SQLiteRawRecipeDto dto)
        {
            return new Recipe
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                EstimatedTime = dto.EstimatedTime,
                IsFavorite = dto.IsFavorite,

                PreviewImage = dto.PreviewImage,

                Ingredients = await QueryEntitiesAndHandleNull(_ingredientsQuery, dto.IngredientIds),
                Pictograms = await QueryEntitiesAndHandleNull(_pictogramsQuery, dto.PictogramIds)
            };
        }

        protected override Task<SQLiteRawRecipeDto> MapEntityToDto(IRecipe entity)
        {
            var dto = new SQLiteRawRecipeDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                EstimatedTime = entity.EstimatedTime,
                IsFavorite = entity.IsFavorite,

                PreviewImage = entity.PreviewImage,

                IngredientIds = ConvertEntityCollectionToString(entity.Ingredients),
                PictogramIds = ConvertEntityCollectionToString(entity.Pictograms)
            };

            return Task.FromResult(dto);
        }

        // TODO: move this to some converter
        // TODO: rename these methods

        private const char Separator = ';';

        private static async Task<IList<T>> QueryEntitiesAndHandleNull<T>(IQueryEntitiesByIds<T> query, string rawIds)
            where T : IEntity
        {
            if (string.IsNullOrEmpty(rawIds))
                return new List<T>();

            var ids = rawIds
                .Split(Separator)
                .Select(Guid.Parse);

            var entities = await query.GetByIds(ids);
            return entities ?? new List<T>();
        }

        private static string ConvertEntityCollectionToString<T>(ICollection<T> entities) where T : IEntity
        {
            if (entities is null)
                return null;

            if (!entities.Any())
                return string.Empty;

            if (entities.Count == 1)
                return entities.First().Id.ToString();

            return entities
                .Select(entity => entity.Id.ToString())
                .Aggregate((id1, id2) => $"{id1}{Separator}{id2}");
        }
    }
}
