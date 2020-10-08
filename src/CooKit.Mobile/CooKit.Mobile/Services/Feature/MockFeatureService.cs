using System;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Mobile.Models;
using CooKit.Mobile.Models.Images;

namespace CooKit.Mobile.Services.Feature
{
    public class MockFeatureService : IFeatureService
    {
        public Task<Recipe> GetFeaturedRecipeAsync()
        {
            var image = new Image(ImageType.Uri, "https://upload.wikimedia.org/wikipedia/commons/e/e9/Egyptian_food_Koshary.jpg");

            var recipe = new Recipe
            {
                Name = "Featured Recipe",
                Description = "Testing Description",
                EstimatedTime = TimeSpan.FromMinutes(30),

                PreviewImage = image,
                Images = Enumerable.Repeat(image, 5).ToList()
            };

            return Task.FromResult(recipe);
        }
    }
}
