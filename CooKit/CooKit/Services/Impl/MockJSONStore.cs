using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Models.Impl.Json;
using Newtonsoft.Json;

namespace CooKit.Services.Impl
{
    public sealed class MockJsonStore : IJsonStore
    {
        private readonly string _ingredientJson;
        private readonly string _pictogramJson;
        private readonly string _recipeJson;

        public MockJsonStore()
        {
            _ingredientJson = JsonConvert.SerializeObject(
                new JsonIngredientInfo
                {
                    Name = "Placeholder Ingredient Name",
                    IconInfo = new JsonImageInfo
                    {
                        LoaderName = "FileImageLoader",
                        Source = "breakfast.png"
                    }
                });

            _pictogramJson = JsonConvert.SerializeObject(
                new JsonPictogramInfo
                {
                    Name = "Placeholder Pictogram Info",
                    Description = "Placeholder Pictogram Description",

                    IconInfo = new JsonImageInfo
                    {
                        LoaderName = "FileImageLoader",
                        Source = "breakfast.png"
                    }
                });

            _recipeJson = JsonConvert.SerializeObject(
                new JsonRecipeInfo
                {
                    Name = "Placeholder Recipe Name",
                    Description = "Placeholder Recipe Description",

                    MainImageInfo = new JsonImageInfo
                    {
                        LoaderName = "FileImageLoader",
                        Source = "food.jpg"
                    },

                    IngredientIds = new []{ Guid.Empty.ToString() },
                    PictogramIds = new []{ Guid.Empty.ToString() },

                    RecipeSteps = new []{ "Placeholder Step 1", "Placeholder Step 2" }
                });
        }

        public string GetIngredientJson(Guid guid) =>
            _ingredientJson;

        public async Task<string> GetIngredientJsonAsync(Guid guid) =>
            await Task.FromResult(_ingredientJson);

        public string GetPictogramJson(Guid guid) =>
            _pictogramJson;

        public async Task<string> GetPictogramJsonAsync(Guid guid) =>
            await Task.FromResult(_pictogramJson);

        public string GetRecipeJson(Guid guid) =>
            _recipeJson;

        public async Task<string> GetRecipeJsonAsync(Guid guid) =>
            await Task.FromResult(_recipeJson);

        public IReadOnlyList<Guid> GetNextRecipeIds() =>
            Enumerable
                .Repeat(Guid.Empty, 10)
                .ToArray();

        public async Task<IReadOnlyList<Guid>> GetNextRecipeIdsAsync() =>
            await Task.FromResult(GetNextRecipeIds());
    }
}
