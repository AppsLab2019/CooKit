using System;
using SQLite;

namespace CooKit.Models.Ingredients
{
    [Table("ingredients")]
    public sealed class IngredientDto : IIngredientDto
    {
        [Column("id")]
        [PrimaryKey, NotNull, Unique]
        public Guid Id { get; set; }

        [NotNull]
        [Column("template_id")]
        public Guid TemplateId { get; set; }

        [Column("note")]
        public string Note { get; set; }

        [Column("quantity")]
        public float Quantity { get; set; }
    }
}
