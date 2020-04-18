using System;
using System.Collections.ObjectModel;
using CooKit.Models;

namespace CooKit.Services.Editor
{
    public sealed class EditorService : IEditorService
    {
        // TODO: move this to a configuration file
        private const string DefaultName = "Unnamed Recipe";
        private const string DefaultDescription = "";
        private const int DefaultEstimatedTime = 0;

        private Recipe _recipe;
        private IRecipeTemplate _template;

        public void CreateTemplate()
        {
            ThrowIfAlreadyHolding();

            _recipe = null;
            _template = CreateDefaultTemplate();
        }

        public IRecipeTemplate GetTemplate()
        {
            return _template;
        }

        public void SetTemplate(Recipe recipe)
        {
            ThrowIfAlreadyHolding();

            _recipe = recipe;
            throw new NotImplementedException();
        }

        public void ClearTemplate()
        {
            _recipe = null;
            _template = null;
        }

        public Recipe TemplateToRecipe()
        {
            throw new NotImplementedException();
        }

        private void ThrowIfAlreadyHolding()
        {
            if (_template is null)
                return;

            throw new Exception();
        }

        // TODO: move this into a factory
        private static IRecipeTemplate CreateDefaultTemplate()
        {
            return new RecipeTemplate
            {
                Name = DefaultName,
                Description = DefaultDescription,
                EstimatedTime = DefaultEstimatedTime,

                Images = new ObservableCollection<string>(),
                Pictograms = new ObservableCollection<Pictogram>(),
                Ingredients = new ObservableCollection<Ingredient>()
            };
        }
    }
}
