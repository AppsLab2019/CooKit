using System;
using CooKit.Models.Units;
using SQLite;

namespace CooKit.Models.Ingredients
{
    [Table("ingredient_templates")]
    public sealed class IngredientTemplate : IIngredientTemplate
    {
        [Column("id")]
        [PrimaryKey, NotNull, Unique]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("icon")]
        public string Icon { get; set; }

        [Column("unit_category")]
        public UnitCategory UnitCategory { get; set; }
    }
}
