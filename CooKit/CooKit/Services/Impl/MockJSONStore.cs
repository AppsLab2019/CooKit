using System;
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
        
        public string GetJson(JsonStoreType type, Guid guid) =>
            type switch
            {
                JsonStoreType.Ingredient => _ingredientJson,
                JsonStoreType.Pictogram => _pictogramJson,
                JsonStoreType.Recipe => _recipeJson,
                _ => null
            };

        public async Task<string> GetJsonAsync(JsonStoreType type, Guid guid) =>
            await Task.Run(() => GetJson(type, guid));
    }
}
