using System;
using SQLite;

namespace CooKit.Models.Impl.SQLite
{
    [Table("steps_text")]
    public sealed class SQLiteTextStepInternalInfo : IStorable
    {
        [Column("id")]
        [PrimaryKey, NotNull, Unique]
        public Guid Id { get; set; }

        [Column("text")]
        public string Text { get; set; }
    }
}
