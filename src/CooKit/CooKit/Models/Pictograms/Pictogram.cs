using System;
using SQLite;

namespace CooKit.Models.Pictograms
{
    [Table("pictograms")]
    public sealed class Pictogram : IPictogram
    {
        [Column("id")]
        [PrimaryKey, NotNull, Unique]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("icon")]
        public string Icon { get; set; }
    }
}
