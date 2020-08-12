using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CooKit.Mobile.Contexts;
using CooKit.Mobile.Contexts.Entities;
using CooKit.Mobile.Models;
using Microsoft.EntityFrameworkCore;

namespace CooKit.Mobile.Repositories.Recipes
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipeContext _recipeContext;
        private readonly IMapper _mapper;

        public RecipeRepository(RecipeContext recipeContext, IMapper mapper)
        {
            _recipeContext = recipeContext;
            _mapper = mapper;
        }

        private IQueryable<RecipeEntity> GetBaseQueryable()
        {
            return _recipeContext.Recipes
                .Include(entity => entity.Ingredients)
                .Include(entity => entity.Pictograms)
                .ThenInclude(entity => entity.Pictogram)
                .Include(entity => entity.Steps);
        }

        public async ValueTask<Recipe> GetRecipeAsync(int id)
        {
            var recipeEntity = await GetBaseQueryable()
                .FirstOrDefaultAsync(entity => entity.Id == id)
                .ConfigureAwait(false);

            return recipeEntity != null ? _mapper.Map<Recipe>(recipeEntity) : null;
        }

        public async ValueTask<IList<Recipe>> GetAllRecipesAsync()
        {
            return await GetBaseQueryable()
                .ProjectTo<Recipe>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
