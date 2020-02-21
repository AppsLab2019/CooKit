namespace CooKit.Models.Impl.Json
{
    public sealed class JsonRecipeInfo
    {
        public string Name;
        public string Description;
        public JsonImageInfo MainImageInfo;

        public string[] IngredientIds;
        public string[] PictogramIds;
    }
}
