using System;
using SQLite;

namespace CooKit.Models.Steps
{
    [Table("steps")]
    public sealed class SQLiteRawStepDto : IEntity
    {
        [Column("id")]
        [PrimaryKey, NotNull, Unique]
        public Guid Id { get; set; }

        [Column("type")]
        public StepType Type { get; set; }

        [Column("data")]
        public string Data { get; set; }
    }
}
