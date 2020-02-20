using CooKit.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Models.Impl;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace CooKit.Services.Impl.Json
{
    public sealed class JsonRecipeStore : IRecipeStore
    {
        public IReadOnlyList<IRecipe> LoadedRecipes => _recipes;

        private readonly IJsonStore _jsonStore;
        private readonly Stack<Guid> _recipeIds;

        private readonly List<IRecipe> _recipes;
        private readonly Dictionary<Guid, IIngredient> _ingredients;
        private readonly Dictionary<Guid, IPictogram> _pictograms;

        public JsonRecipeStore(IJsonStore jsonStore, string mainJson)
        {
            _jsonStore = jsonStore;

            var recipeIds = JArray
                .Parse(mainJson)
                .ToObject<string[]>()
                .Select(Guid.Parse);

            _recipeIds = new Stack<Guid>(recipeIds);

            _recipes = new List<IRecipe>();
            _ingredients = new Dictionary<Guid, IIngredient>();
            _pictograms = new Dictionary<Guid, IPictogram>();
        }

        public IRecipe LoadRecipe()
        {
            if (_recipeIds.Count == 0)
                return null;

            var recipeJson = _jsonStore.GetJson(JsonStoreType.Recipe, _recipeIds.Pop());

            if (recipeJson is null)
                return null;

            var recipeInfo = JsonConvert.DeserializeObject<DeserializedRecipeInfo>(recipeJson);

            var ingredients = recipeInfo.Ingredients
                .Select(Guid.Parse)
                .Select(LoadIngredient)
                .ToArray();

            var pictograms = recipeInfo.Pictograms
                .Select(Guid.Parse)
                .Select(LoadPictogram)
                .ToArray();

            // TODO: replace with custom type ? (maybe wrapper for DeserializedRecipeInfo)
            var recipe = new MockRecipe(recipeInfo.Name, recipeInfo.Description,
                ImageSource.FromFile("food.jpg"),
                pictograms, ingredients,
                new []{ "Step 1?", "Step 2?" });

            _recipes.Add(recipe);
            return recipe;
        }

        public async Task<IRecipe> LoadRecipeAsync() =>
            await Task.Run(LoadRecipe);

        public IReadOnlyList<IRecipe> LoadRecipes(int count)
        {
            var list = new List<IRecipe>(count);

            for (var i = 0; i < count; i++)
            {
                var loadedRecipe = LoadRecipe();

                if (loadedRecipe is null)
                    break;

                list.Add(loadedRecipe);
            }

            return list;
        }

        public async Task<IReadOnlyList<IRecipe>> LoadRecipesAsync(int count) =>
            await Task.Run(() => LoadRecipes(count));

        private IIngredient LoadIngredient(Guid guid)
        {
            if (_ingredients.ContainsKey(guid))
                return _ingredients[guid];

            var ingredientJson = _jsonStore.GetJson(JsonStoreType.Ingredient, guid);

            // TODO: replace me with placeholder ingredient
            if (ingredientJson is null)
                return MockIngredient.Example;

            var ingredientInfo = JsonConvert.DeserializeObject<DeserializedIngredientInfo>(ingredientJson);

            // TODO: replace with custom type ? (maybe wrapper for DeserializedIngredientInfo)
            var ingredient = new MockIngredient(ingredientInfo.Name, ImageSource.FromFile("breakfast.png"));

            _ingredients[guid] = ingredient;
            return ingredient;
        }

        private IPictogram LoadPictogram(Guid guid)
        {
            if (_pictograms.ContainsKey(guid))
                return _pictograms[guid];

            var pictogramJson = _jsonStore.GetJson(JsonStoreType.Pictogram, guid);

            // TODO: replace me with placeholder pictogram
            if (pictogramJson is null)
                return MockPictogram.Example;

            var pictogramInfo = JsonConvert.DeserializeObject<DeserializedPictogramInfo>(pictogramJson);

            // TODO: replace with custom type ? (maybe wrapper for DeserializedPictogramInfo)
            var pictogram = new MockPictogram(pictogramInfo.Name, pictogramInfo.Description,
                ImageSource.FromFile("breakfast.png"));

            _pictograms[guid] = pictogram;
            return pictogram;
        }
    }
}
