namespace CooKit.Models.Steps
{
    public interface IRecipeStep : IStorable
    {
        RecipeStepType Type { get; }
    }
}
