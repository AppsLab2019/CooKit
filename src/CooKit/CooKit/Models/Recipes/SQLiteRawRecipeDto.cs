using System;
using SQLite;

namespace CooKit.Models.Recipes
{
    [Table("recipes")]
    public sealed class SQLiteRawRecipeDto : IEntity
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
        public string PreviewImage { get; set; }

        [Column("images")]
        public string Images { get; set; }

        [Column("pictogram_ids")]
        public string PictogramIds { get; set; }

        [Column("ingredient_ids")]
        public string IngredientIds { get; set; }

        //[Column("step_ids")]
        //public string StepIds { get; set; }
    }
}
