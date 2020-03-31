using System;
using SQLite;

namespace CooKit.Models.Impl.SQLite
{
    [Table("recipes")]
    public sealed class SQLiteRecipeInternalInfo : IStorable
    {
        [Column("id")]
        [PrimaryKey, NotNull, Unique]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }

        [Column("image_loader")]
        public string ImageLoader { get; set; }
        [Column("image_source")]
        public string ImageSource { get; set; }
        [Column("required_time")]
        public TimeSpan RequiredTime { get; set; }

        [Column("ingredients")]
        public string Ingredients { get; set; }
        [Column("pictograms")]
        public string Pictograms { get; set; }
        [Column("steps")]
        public string Steps { get; set; }
    }
}
