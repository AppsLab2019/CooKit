using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Mobile.Models;
using CooKit.Mobile.Repositories.Recipes;

namespace CooKit.Mobile.Viewmodels.Editor
{
    public class EditorSelectionViewmodel : ParameterlessBaseViewmodel
    {
        private readonly IRecipeRepository _recipeRepository;

        public EditorSelectionViewmodel(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public IList<Recipe> Recipes
        {
            get => _recipes;
            set => OnPropertyChanged(ref _recipes, value);
        }

        private IList<Recipe> _recipes;

        protected override async Task InitializeAsync()
        {
            Recipes = await _recipeRepository.GetAllRecipesAsync();
        }
    }
}
