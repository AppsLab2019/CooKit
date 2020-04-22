using System;
using System.Collections.Generic;
using CooKit.Models.Recipes;

namespace CooKit.Services.Editor
{
    public sealed class EditorService : IEditorService
    {
        // TODO: move this to a configuration file
        private const string DefaultName = "Unnamed Recipe";
        private const string DefaultDescription = "";
        private const int DefaultEstimatedTime = 0;

        private IRecipe _recipe;

        public void CreateNewRecipe()
        {
            // TODO: move this to factory
            _recipe = new Recipe
            {
                Name = DefaultName,
                Description = DefaultDescription,
                EstimatedTime = DefaultEstimatedTime,
                IsFavorite = false,

                PreviewImage = null,
                //Images = new List<string>(),

                IngredientIds = new List<Guid>(),
                PictogramIds = new List<Guid>()
            };
        }

        public IRecipe GetWorkingRecipe()
        {
            return _recipe;
        }

        public void SetWorkingRecipe(IRecipe recipe)
        {
            _recipe = recipe;
        }

        public void ClearWorkingRecipe()
        {
            _recipe = null;
        }
    }
}
