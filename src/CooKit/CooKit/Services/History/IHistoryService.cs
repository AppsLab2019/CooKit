using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Models.Recipes;

namespace CooKit.Services.History
{
    public interface IHistoryService
    {
        Task<IList<IRecipe>> GetLastVisitedRecipes();
    }
}
