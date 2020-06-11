using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Converters.Json;
using CooKit.Models.Ingredients;
using CooKit.Models.Pictograms;
using CooKit.Models.Recipes;
using CooKit.Models.Steps;
using CooKit.Services.Queries;

namespace CooKit.Repositories.Recipes
{
    public sealed class SQLiteRecipeRepository : MappingRepository<IRecipe, SQLiteRawRecipeDto>, IRecipeRepository
    {
        private readonly IJsonConverter _converter;
        private readonly IQueryEntitiesByIds<IPictogram> _queryPictograms;
        private readonly IQueryEntitiesByIds<IIngredientTemplate> _queryTemplates;

        public SQLiteRecipeRepository(IJsonConverter converter,
            IQueryEntitiesByIds<IPictogram> queryPictograms,
            IQueryEntitiesByIds<IIngredientTemplate> queryTemplates,
            ISQLiteRawRecipeDtoRepository repository) : base(repository)
        {
            if (converter is null)
                throw new ArgumentNullException(nameof(converter));

            if (queryPictograms is null)
                throw new ArgumentNullException(nameof(queryPictograms));

            if (queryTemplates is null)
                throw new ArgumentNullException(nameof(queryTemplates));

            _converter = converter;
            _queryPictograms = queryPictograms;
            _queryTemplates = queryTemplates;
        }

        protected override async Task<IRecipe> MapDtoToEntity(SQLiteRawRecipeDto dto)
        {
            var ingredientsTask = DeserializeIngredients(dto.Ingredients);
            var pictogramsTask = DeserializePictograms(dto.Pictograms);

            var recipe = new Recipe
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                EstimatedTime = dto.EstimatedTime,
                IsFavorite = dto.IsFavorite,

                PreviewImage = dto.PreviewImage,
                Images = DeserializeImages(dto.Images),

                Steps = DeserializeSteps(dto.Steps)
            };

            await Task.WhenAll(ingredientsTask, pictogramsTask);

            recipe.Ingredients = ingredientsTask.Result;
            recipe.Pictograms = pictogramsTask.Result;

            return recipe;
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
                Images = _converter.Serialize(entity.Images),

                Ingredients = SerializeIngredients(entity.Ingredients),
                Pictograms = SerializePictograms(entity.Pictograms),
                Steps = SerializeSteps(entity.Steps)
            };

            return Task.FromResult(dto);
        }

        private string SerializeIngredients(IEnumerable<IIngredient> ingredients)
        {
            if (ingredients is null)
                return null;

            var dtos = ingredients
                .Select(ingredient => new IngredientDto
                {
                    TemplateId = ingredient.Template.Id,
                    Note = ingredient.Note,
                    Quantity = ingredient.Quantity
                });

            return _converter.Serialize(dtos);
        }

        private string SerializePictograms(IEnumerable<IPictogram> pictograms)
        {
            if (pictograms is null)
                return null;

            var ids = pictograms.Select(pictogram => pictogram.Id);
            return _converter.Serialize(ids);
        }

        private string SerializeSteps(IEnumerable<IStep> steps)
        {
            return steps is null ? null : _converter.Serialize(steps);
        }

        private IList<string> DeserializeImages(string rawImages)
        {
            if (string.IsNullOrEmpty(rawImages))
                return new List<string>();

            return _converter.Deserialize<List<string>>(rawImages);
        }

        private async Task<IList<IIngredient>> DeserializeIngredients(string rawIngredients)
        {
            if (string.IsNullOrEmpty(rawIngredients))
                return new List<IIngredient>();

            var dtos = _converter.Deserialize<IngredientDto[]>(rawIngredients);
            var templates = await _queryTemplates.GetByIds(dtos.Select(dto => dto.TemplateId));

            return dtos
                .Select(dto => new Ingredient
                {
                    Template = templates.First(template => template.Id == dto.TemplateId), 
                    Note = dto.Note, 
                    Quantity = dto.Quantity
                })
                .Cast<IIngredient>()
                .ToList();
        }

        private Task<IList<IPictogram>> DeserializePictograms(string rawPictograms)
        {
            if (string.IsNullOrEmpty(rawPictograms))
                return GetEmptyListTask<IPictogram>();

            var ids = _converter.Deserialize<Guid[]>(rawPictograms);
            return _queryPictograms.GetByIds(ids);
        }

        private IList<IStep> DeserializeSteps(string rawSteps)
        {
            if (string.IsNullOrEmpty(rawSteps))
                return new List<IStep>();

            return _converter.Deserialize<List<IStep>>(rawSteps);
        }

        private static Task<IList<T>> GetEmptyListTask<T>()
        {
            IList<T> list = new List<T>();
            return Task.FromResult(list);
        }
    }
}
