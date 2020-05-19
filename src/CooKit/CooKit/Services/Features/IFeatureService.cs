using System.Threading.Tasks;
using CooKit.Models.Recipes;

namespace CooKit.Services.Features
{
    public interface IFeatureService
    {
        Task<IRecipe> GetFeaturedRecipe();
    }
}
