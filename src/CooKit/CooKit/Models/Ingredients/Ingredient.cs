using System;
using SQLite;

namespace CooKit.Models.Ingredients
{
    [Table("ingredients")]
    public sealed class Ingredient : IIngredient
    {
        [Column("id")]
        [PrimaryKey, NotNull, Unique]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("icon")]
        public string Icon { get; set; }
    }
}
