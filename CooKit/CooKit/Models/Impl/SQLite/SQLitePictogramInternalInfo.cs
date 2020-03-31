using System;
using SQLite;

namespace CooKit.Models.Impl.SQLite
{
    [Table("pictograms")]
    public sealed class SQLitePictogramInternalInfo : IStorable
    {
        [Column("id")]
        [PrimaryKey, NotNull, Unique]
        public Guid Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }

        [Column("image_loader")]
        public string ImageLoader { get; set; }
        [Column("image_source")]
        public string ImageSource { get; set; }
    }
}
