namespace CooKit.Models.Ingredients
{
    public interface IIngredient
    {
        IIngredientTemplate Template { get; set; }
        string Note { get; set; }
        float Quantity { get; set; }
    }
}
