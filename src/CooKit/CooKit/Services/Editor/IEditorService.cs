using CooKit.Models;

namespace CooKit.Services.Editor
{
    public interface IEditorService
    {
        void CreateTemplate();
        IRecipeTemplate GetTemplate();
        void SetTemplate(Recipe recipe);
        void ClearTemplate();

        Recipe TemplateToRecipe();
    }
}
