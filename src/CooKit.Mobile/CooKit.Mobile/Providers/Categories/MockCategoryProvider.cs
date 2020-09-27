using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Mobile.Models;
using CooKit.Mobile.Models.Images;

namespace CooKit.Mobile.Providers.Categories
{
    public class MockCategoryProvider : ICategoryProvider
    {
        public Task<IList<ICategory>> GetCategoriesAsync()
        {
            var recipe = new Recipe
            {
                Name = "Category Recipe",
                PreviewImage = new Image(ImageType.Uri, "https://www.foodiesfeed.com/wp-content/uploads/2019/06/top-view-for-box-of-2-burgers-home-made.jpg")
            };

            var recipes = Enumerable.Repeat(recipe, 5).ToList();
            ICategory category = new MockCategory("Test Category", recipes);
            IList<ICategory> categories = Enumerable.Repeat(category, 3).ToList();

            return Task.FromResult(categories);
        }
    }
}
