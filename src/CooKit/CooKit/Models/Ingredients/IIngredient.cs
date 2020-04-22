namespace CooKit.Models.Ingredients
{
    public interface IIngredient : IEntity
    {
        string Name { get; set; }
        string Icon { get; set; }
    }
}
