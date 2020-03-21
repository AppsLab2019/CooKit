using System;
using SQLite;

namespace CooKit.Models.Impl.SQLite.Steps
{
    [Table("textSteps")]
    public sealed class SQLiteTextRecipeStepInfo
    {
        [PrimaryKey, Unique, NotNull]
        public Guid Id { get; set; }
        
        [NotNull]
        public string Text { get; set; }
    }
}
