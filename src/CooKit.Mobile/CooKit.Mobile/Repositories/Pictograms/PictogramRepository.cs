using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Mobile.Contexts;
using CooKit.Mobile.Models;
using Microsoft.EntityFrameworkCore;

namespace CooKit.Mobile.Repositories.Pictograms
{
    public class PictogramRepository : IPictogramRepository
    {
        private readonly RecipeContext _recipeContext;

        public PictogramRepository(RecipeContext recipeContext)
        {
            _recipeContext = recipeContext;
        }

        public ValueTask<Pictogram> GetPictogramAsync(int id)
        {
            return _recipeContext.Pictograms.FindAsync(id);
        }

        public async ValueTask<IList<Pictogram>> GetAllPictogramsAsync()
        {
            return await _recipeContext.Pictograms.ToListAsync();
        }
    }
}
