using System;
using SQLite;

namespace CooKit.Models
{
    [Table("ingredients")]
    public sealed class Ingredient : IEntity
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
