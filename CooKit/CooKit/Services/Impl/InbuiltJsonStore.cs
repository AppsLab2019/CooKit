using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CooKit.Services.Impl
{
    public sealed class InbuiltJsonStore : IJsonStore
    {
        private readonly Stack<Guid> _recipeIds;

        public InbuiltJsonStore()
        {
            using var recipeIdsStream = OpenStream("CooKit.Recipes.recipeIds.json");
            var recipeIdsJson = new StreamReader(recipeIdsStream).ReadToEnd();

            var recipeIds = new JArray(recipeIdsJson)
                .Cast<string>()
                .Select(Guid.Parse);

            _recipeIds = new Stack<Guid>(recipeIds);
        }

        public string GetIngredientJson(Guid guid) =>
            ReadStringFromResource($"CooKit.Recipes.Ingredients.{guid}.json");

        public async Task<string> GetIngredientJsonAsync(Guid guid) =>
            await ReadStringFromResourceAsync($"CooKit.Recipes.Ingredients.{guid}.json");

        public string GetPictogramJson(Guid guid) =>
            ReadStringFromResource($"CooKit.Recipes.Pictograms.{guid}.json");

        public async Task<string> GetPictogramJsonAsync(Guid guid) =>
            await ReadStringFromResourceAsync($"CooKit.Recipes.Pictograms.{guid}.json");

        public string GetRecipeJson(Guid guid) =>
            ReadStringFromResource($"CooKit.Recipes.Recipe.{guid}.json");

        public async Task<string> GetRecipeJsonAsync(Guid guid) =>
            await ReadStringFromResourceAsync($"CooKit.Recipes.Recipe.{guid}.json");

        public IReadOnlyList<Guid> GetNextRecipeIds()
        {
            var ids = new Guid[_recipeIds.Count >= 10 ? 10 : _recipeIds.Count];

            for (var i = 0; i < ids.Length; i++)
                ids[i] = _recipeIds.Pop();

            return ids;
        }

        public async Task<IReadOnlyList<Guid>> GetNextRecipeIdsAsync() =>
            await Task.Run(GetNextRecipeIds);

        private static string ReadStringFromResource(string resourceName)
        {
            using var stream = OpenStream(resourceName);
            return new StreamReader(stream).ReadToEnd();
        }

        private static async Task<string> ReadStringFromResourceAsync(string resourceName)
        {
            using var stream = OpenStream(resourceName);
            return await new StreamReader(stream).ReadToEndAsync();
        }

        private static Stream OpenStream(string resourceName) =>
            Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream(resourceName)
            ?? throw new Exception();
    }
}
