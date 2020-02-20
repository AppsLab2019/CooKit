using CooKit.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CooKit.Services.Impl.Json
{
    public sealed class JSONRecipeStore : IRecipeStore
    {
        public IReadOnlyList<IRecipe> LoadedRecipes { get; }

        private readonly IJSONStore _jsonStore;
        private readonly Stack<Guid> _recipeIds;

        private readonly Dictionary<Guid, IIngredient> _ingredients;
        private readonly Dictionary<Guid, IPictogram> _pictograms;

        public JSONRecipeStore(IJSONStore jsonStore, string mainJSON)
        {
            _jsonStore = jsonStore;
            _recipeIds = new Stack<Guid>(JsonConvert.DeserializeObject<Guid[]>(mainJSON));

            _ingredients = new Dictionary<Guid, IIngredient>();
            _pictograms = new Dictionary<Guid, IPictogram>();
        }

        public IRecipe LoadRecipe()
        {
            var recipeJson = _jsonStore.GetJSON(JSONStoreType.Recipe, _recipeIds.Pop());
            var recipeInfo = JsonConvert.DeserializeObject<DeserializedRecipeInfo>(recipeJson);

            // TODO: rework this
            foreach (var ingredientId in recipeInfo.Ingredients)
                if (!_ingredients.ContainsKey(ingredientId))
                    _ingredients[ingredientId] = LoadIngredient(ingredientId);

            foreach (var pictogramId in recipeInfo.Pictograms)
                if (!_pictograms.ContainsKey(pictogramId))
                    _pictograms[pictogramId] = LoadPictogram(pictogramId);

            throw new NotImplementedException();
        }

        public async Task<IRecipe> LoadRecipeAsync() =>
           throw new NotImplementedException();

        public IReadOnlyList<IRecipe> LoadRecipes(int count) => 
            throw new NotImplementedException();

        public async Task<IReadOnlyList<IRecipe>> LoadRecipesAsync(int count) =>
            throw new NotImplementedException();

        private IIngredient LoadIngredient(Guid guid) => 
            throw new NotImplementedException();

        private IPictogram LoadPictogram(Guid guid) => 
            throw new NotImplementedException();
    }
}
