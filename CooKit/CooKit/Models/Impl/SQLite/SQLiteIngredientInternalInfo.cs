using System;
using SQLite;

namespace CooKit.Models.Impl.SQLite
{
    [Table("ingredients")]
    public sealed class SQLiteIngredientInternalInfo : IStorable
    {
        [Column("id")]
        [PrimaryKey, NotNull, Unique]
        public Guid Id { get; set; }
        [Column("name")]
        public string Name { get; set; }

        [Column("image_loader")]
        public string ImageLoader { get; set; }
        [Column("image_source")]
        public string ImageSource { get; set; }
    }
}
