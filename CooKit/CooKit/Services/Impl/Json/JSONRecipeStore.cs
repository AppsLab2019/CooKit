using CooKit.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Models.Impl;
using CooKit.Models.Impl.Json;

namespace CooKit.Services.Impl.Json
{
    public sealed class JsonRecipeStore : IRecipeStore
    {
        public IReadOnlyList<IRecipe> LoadedRecipes => _recipes;

        private readonly IJsonStore _jsonStore;
        private readonly IImageStore _imageStore;
        private readonly Stack<Guid> _recipeIds;

        private readonly List<IRecipe> _recipes;
        private readonly Dictionary<Guid, IIngredient> _ingredients;
        private readonly Dictionary<Guid, IPictogram> _pictograms;

        public JsonRecipeStore(IJsonStore jsonStore, IImageStore imageStore)
        {
            _jsonStore = jsonStore;
            _imageStore = imageStore;
            _recipeIds = new Stack<Guid>();

            _recipes = new List<IRecipe>();
            _ingredients = new Dictionary<Guid, IIngredient>();
            _pictograms = new Dictionary<Guid, IPictogram>();
        }

        public IRecipe LoadRecipe()
        {
            if (_recipeIds.Count == 0)
            {
                var recipeIds = _jsonStore.GetNextRecipeIds();

                if (!recipeIds.Any())
                    return null;

                foreach (var recipeId in recipeIds)
                    _recipeIds.Push(recipeId);
            }

            var recipeJson = _jsonStore.GetRecipeJson(_recipeIds.Pop());

            if (recipeJson is null)
                return null;

            var recipeInfo = JsonConvert.DeserializeObject<JsonRecipeInfo>(recipeJson);

            var mainImageInfo = recipeInfo.MainImageInfo;
            var mainImage = _imageStore.LoadImage(
                mainImageInfo.LoaderName,
                mainImageInfo.Source);

            var ingredients = recipeInfo.IngredientIds
                .Select(Guid.Parse)
                .Select(LoadIngredient)
                .ToArray();

            var pictograms = recipeInfo.PictogramIds
                .Select(Guid.Parse)
                .Select(LoadPictogram)
                .ToArray();

            // TODO: replace with custom type ? (maybe wrapper for DeserializedRecipeInfo)
            var recipe = new MockRecipe(recipeInfo.Name, recipeInfo.Description,
                mainImage, pictograms, ingredients, recipeInfo.RecipeSteps);

            _recipes.Add(recipe);
            return recipe;
        }

        // TODO: properly implement
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

        // TODO: properly implement
        public async Task<IReadOnlyList<IRecipe>> LoadRecipesAsync(int count) =>
            await Task.Run(() => LoadRecipes(count));

        private IIngredient LoadIngredient(Guid guid)
        {
            if (_ingredients.ContainsKey(guid))
                return _ingredients[guid];

            var ingredientJson = _jsonStore.GetIngredientJson(guid);

            // TODO: replace me with placeholder ingredient
            if (ingredientJson is null)
                return MockIngredient.Example;

            var ingredientInfo = JsonConvert.DeserializeObject<JsonIngredientInfo>(ingredientJson);
            var iconInfo = ingredientInfo.IconInfo;

            // TODO: replace with custom type ? (maybe wrapper for DeserializedIngredientInfo)
            var ingredient = new MockIngredient(ingredientInfo.Name,
                _imageStore.LoadImage(iconInfo.LoaderName, iconInfo.Source));

            _ingredients[guid] = ingredient;
            return ingredient;
        }

        private IPictogram LoadPictogram(Guid guid)
        {
            if (_pictograms.ContainsKey(guid))
                return _pictograms[guid];

            var pictogramJson = _jsonStore.GetPictogramJson(guid);

            // TODO: replace me with placeholder pictogram
            if (pictogramJson is null)
                return MockPictogram.Example;

            var pictogramInfo = JsonConvert.DeserializeObject<JsonPictogramInfo>(pictogramJson);
            var iconInfo = pictogramInfo.IconInfo;

            // TODO: replace with custom type ? (maybe wrapper for DeserializedPictogramInfo)
            var pictogram = new MockPictogram(pictogramInfo.Name, pictogramInfo.Description,
                _imageStore.LoadImage(iconInfo.LoaderName, iconInfo.Source));

            _pictograms[guid] = pictogram;
            return pictogram;
        }
    }
}
