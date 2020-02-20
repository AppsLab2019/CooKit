using System;
using System.Threading.Tasks;
using CooKit.Services.Impl.Json;
using Newtonsoft.Json;

namespace CooKit.Services.Impl
{
    public sealed class MockJsonStore : IJsonStore
    {
        public string GetJson(JsonStoreType type, Guid guid)
        {
            if (type == JsonStoreType.Ingredient)
            {
                var ingredient = new DeserializedIngredientInfo
                {
                    Name = "Placeholder Ingredient Name"
                };

                return JsonConvert.SerializeObject(ingredient);
            }
            if (type == JsonStoreType.Pictogram)
            {
                var pictogram = new DeserializedPictogramInfo
                {
                    Name = "Placeholder Pictogram Info",
                    Description = "Placeholder Pictogram Description"
                };

                return JsonConvert.SerializeObject(pictogram);
            }
            
            var recipe = new DeserializedRecipeInfo
            {
                Name = "Placeholder Recipe Name",
                Description = "Placeholder Recipe Description",
                Ingredients = new[] {Guid.Empty.ToString()},
                Pictograms = new[] {Guid.Empty.ToString()}
            };

            return JsonConvert.SerializeObject(recipe);
        }

        public async Task<string> GetJsonAsync(JsonStoreType type, Guid guid) =>
            await Task.Run(() => GetJson(type, guid));
    }
}
