using System;
using SQLite;

namespace CooKit.Models.Pictograms
{
    [Table("Pictograms")]
    public sealed class Pictogram : IPictogram
    {
        [PrimaryKey, NotNull, Unique]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
