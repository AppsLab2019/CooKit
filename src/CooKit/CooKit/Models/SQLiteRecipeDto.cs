using System;
using SQLite;

namespace CooKit.Models
{
    [Table("recipes")]
    public sealed class SQLiteRecipeDto : IEntity
    {
        [Column("id")]
        [PrimaryKey, NotNull, Unique]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("estimated_time")]
        public int EstimatedTime { get; set; }

        [Column("is_favorite")]
        public bool IsFavorite { get; set; }

        [Column("preview_image")]
        public Uri PreviewImage { get; set; }

        //[Column("images")]
        //public string Images { get; set; }

        [Column("pictograms")]
        public string Pictograms { get; set; }

        [Column("ingredients")]
        public string Ingredients { get; set; }
    }
}
