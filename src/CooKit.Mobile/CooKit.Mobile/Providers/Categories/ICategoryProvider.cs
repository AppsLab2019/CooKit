using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Mobile.Models;

namespace CooKit.Mobile.Providers.Categories
{
    public interface ICategoryProvider
    {
        Task<IList<ICategory>> GetCategoriesAsync();
    }
}
