using System;
using System.Threading.Tasks;
using CooKit.Models.Editor.Recipe;
using CooKit.Services.Stores.Ingredients;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditIngredientsViewModel : ViewModel
    {
        private IEditorRecipe _recipe;
        private readonly IIngredientTemplateStore _store;

        public EditIngredientsViewModel(IIngredientTemplateStore store)
        {
            if (store is null)
                throw new ArgumentNullException(nameof(store));

            _store = store;
        }

        public override Task InitializeAsync(object parameter)
        {
            _recipe = parameter as IEditorRecipe;
            
            if (_recipe is null)
                throw new ArgumentException(nameof(parameter));

            throw new NotImplementedException();
        }
    }
}
