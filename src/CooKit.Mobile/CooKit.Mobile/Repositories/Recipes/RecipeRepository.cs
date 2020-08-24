using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Mobile.Contexts;
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
    }
}
