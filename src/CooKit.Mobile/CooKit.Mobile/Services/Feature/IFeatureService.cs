using System.Threading.Tasks;
using CooKit.Mobile.Models;

namespace CooKit.Mobile.Services.Feature
{
    public interface IFeatureService
    {
        Task<Recipe> GetFeaturedRecipeAsync();
    }
}
