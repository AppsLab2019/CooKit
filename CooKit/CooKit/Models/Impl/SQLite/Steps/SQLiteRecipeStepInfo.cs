using System;
using CooKit.Models.Steps;
using SQLite;

namespace CooKit.Models.Impl.SQLite.Steps
{
    [Table("steps")]
    public sealed class SQLiteRecipeStepInfo
    {
        [PrimaryKey, NotNull, Unique]
        public Guid Id { get; set; }

        [NotNull]
        public RecipeStepType Type { get; set; }
    }
}
