using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Mobile.Models;
using CooKit.Mobile.Models.Images;
using CooKit.Mobile.Models.Ingredients;
using CooKit.Mobile.Models.Steps;

namespace CooKit.Mobile.Repositories.Recipes
{
    public class MockRecipeRepository : IRecipeRepository
    {
        private readonly IList<Recipe> _recipes;

        public MockRecipeRepository()
        {
            _recipes = Enumerable.Range(1, 30)
                .Select(CreateRecipeFromTemplate)
                .ToList();
        }

        private static Recipe CreateRecipeFromTemplate(int id)
        {
            var image = new Image(ImageType.Uri,
                "https://upload.wikimedia.org/wikipedia/commons/thumb/3/35/Toast-3.jpg/800px-Toast-3.jpg");

            var ingredient = new Ingredient { Text = "Test Ingredient!" };
            var pictogram = new Pictogram
            {
                Name = "Test Pictogram",
                Description = "Pictogram Description!",
                Icon = image
            };

            return new Recipe
            {
                Id = id,
                Name = $"Test Recipe {id}",
                Description = "Description sample!",
                EstimatedTime = TimeSpan.FromMinutes(30),
                
                PreviewImage = image,
                Images = Enumerable.Repeat(image, 5).ToArray(),

                Pictograms = Enumerable.Repeat(pictogram, 3).ToArray(),
                Ingredients = Enumerable.Repeat(ingredient, 5).ToArray(),
                Steps = new List<Step>
                {
                    new TextStep { Text = "Hello World!" },
                    new ImageStep { Image = image }
                }
            };
        }

        public ValueTask<Recipe> GetRecipeAsync(int id)
        {
            var recipe = _recipes.FirstOrDefault(r => r.Id == id);
            return new ValueTask<Recipe>(recipe);
        }

        public ValueTask<IList<Recipe>> GetAllRecipesAsync()
        {
            return new ValueTask<IList<Recipe>>(_recipes);
        }
    }
}
