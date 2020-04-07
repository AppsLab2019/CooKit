using System;
using SQLite;

namespace CooKit.Models
{
    [Table("pictograms")]
    public sealed class Pictogram : IEntity
    {
        [Column("id")]
        [PrimaryKey, NotNull, Unique]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("icon")]
        public Uri Icon { get; set; }
    }
}
