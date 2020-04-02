using System;
using CooKit.Models.Steps;
using SQLite;

namespace CooKit.Models.Impl.SQLite
{
    [Table("steps")]
    public sealed class SQLiteStepInternalInfo : IStorable
    {
        [Column("id")]
        [PrimaryKey, NotNull, Unique]
        public Guid Id { get; set; }
        [NotNull]
        [Column("type")]
        public StepType Type { get; set; }
    }
}
