using System;
using SQLite;

namespace CooKit.Models.Impl.SQLite
{
    [Table("steps_image")]
    public sealed class SQLiteImageStepInternalInfo : IStorable
    {
        [Column("id")]
        [PrimaryKey, NotNull, Unique]
        public Guid Id { get; set; }
        
        [Column("image_loader")]
        public string ImageLoader { get; set; }
        [Column("image_source")]
        public string ImageSource { get; set; }
    }
}
