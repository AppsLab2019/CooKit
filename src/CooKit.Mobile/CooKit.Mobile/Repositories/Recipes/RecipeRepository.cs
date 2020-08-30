using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Mobile.Contexts;
using CooKit.Mobile.Contexts.Entities;
using CooKit.Mobile.Models;
using Microsoft.EntityFrameworkCore;

namespace CooKit.Mobile.Repositories.Recipes
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipeContext _recipeContext;

        public RecipeRepository(RecipeContext recipeContext)
        {
            _recipeContext = recipeContext;
        }

        private IQueryable<Recipe> GetBaseQueryable()
        {
            return _recipeContext.Recipes
                .Include(entity => entity.Ingredients)
                .Include(entity => entity.Pictograms)
                .ThenInclude(entity => entity.Pictogram)
                .Include(entity => entity.Steps)
                .Select(entity => new Recipe
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                    EstimatedTime = entity.EstimatedTime,

                    PreviewImage = entity.PreviewImage,
                    Images = entity.Images,

                    Pictograms = entity.Pictograms
                        .Select(pair => pair.Pictogram)
                        .ToList(),

                    Ingredients = entity.Ingredients,
                    Steps = entity.Steps
                });
        }

        public async ValueTask AddRecipeAsync(Recipe recipe)
        {
            if (recipe == null)
                throw new ArgumentNullException(nameof(recipe));

            // TODO: maybe check if recipe contains id?

            var entity = RecipeToEntity(recipe);
            var entry = await _recipeContext.Recipes.AddAsync(entity);

            recipe.Id = entry.Entity.Id;
            await _recipeContext.SaveChangesAsync();
        }

        public ValueTask UpdateRecipeAsync(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Recipe> GetRecipeAsync(int id)
        {
            var queryTask = GetBaseQueryable()
                .FirstOrDefaultAsync(entity => entity.Id == id);

            return new ValueTask<Recipe>(queryTask);
        }

        public async ValueTask<IList<Recipe>> GetAllRecipesAsync()
        {
            return await GetBaseQueryable().ToListAsync();
        }

        private static RecipeEntity RecipeToEntity(Recipe recipe)
        {
            var entity = new RecipeEntity
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                EstimatedTime = recipe.EstimatedTime,

                PreviewImage = recipe.PreviewImage,
                Images = recipe.Images,

                Ingredients = recipe.Ingredients,
                Steps = recipe.Steps
            };

            entity.Pictograms = recipe.Pictograms
                .Select(pictogram => new RecipePictogramPair(entity, pictogram))
                .ToList();

            return entity;
        }
    }
}
