using System;
using SQLite;

namespace CooKit.Models.Recipes
{
    [Table("Recipes")]
    public sealed class SQLiteRawRecipeDto : IEntity
    {
        [PrimaryKey, NotNull, Unique]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int EstimatedTime { get; set; }

        public bool IsFavorite { get; set; }

        public string PreviewImage { get; set; }
        public string Images { get; set; }

        public string Ingredients { get; set; }
        public string Pictograms { get; set; }
        public string Steps { get; set; }
    }
}
