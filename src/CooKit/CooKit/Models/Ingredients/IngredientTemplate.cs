using System;
using CooKit.Models.Units;
using SQLite;

namespace CooKit.Models.Ingredients
{
    [Table("IngredientTemplates")]
    public sealed class IngredientTemplate : IIngredientTemplate
    {
        [PrimaryKey, NotNull, Unique]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Icon { get; set; }
        public UnitCategory UnitCategory { get; set; }
    }
}
