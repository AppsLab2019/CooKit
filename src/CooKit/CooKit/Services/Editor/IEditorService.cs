using CooKit.Models.Recipes;

namespace CooKit.Services.Editor
{
    public interface IEditorService
    {
        void CreateNewRecipe();
        IRecipe GetWorkingRecipe();
        void SetWorkingRecipe(IRecipe recipe);
        void ClearWorkingRecipe();
    }
}
