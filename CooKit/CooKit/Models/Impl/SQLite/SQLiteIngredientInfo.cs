using SQLite;

namespace CooKit.Models.Impl.SQLite
{
    public sealed class SQLiteIngredientInfo
    {
        [PrimaryKey, NotNull, Unique]
        public string Id { get; set; }
        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string ImageLoader { get; set; }
        [NotNull]
        public string ImageSource { get; set; }
    }
}
