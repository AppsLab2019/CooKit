using SQLite;

namespace CooKit.Models.Impl.SQLite
{
    public sealed class SQLiteRecipeInfo
    {
        [PrimaryKey, NotNull, Unique]
        public string Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public string Description { get; set; }
        
        [NotNull]
        public string ImageLoader { get; set; }
        [NotNull]
        public string ImageSource { get; set; }

        [NotNull]
        public string IngredientIds { get; set; }
        [NotNull]
        public string PictogramIds { get; set; }
        [NotNull]
        public string StepIds { get; set; }

        public SQLiteRecipeInfo() { }
    }
}
