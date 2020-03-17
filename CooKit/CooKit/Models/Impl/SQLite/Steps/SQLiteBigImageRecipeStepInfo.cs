using System;
using SQLite;

namespace CooKit.Models.Impl.SQLite.Steps
{
    public sealed class SQLiteBigImageRecipeStepInfo
    {
        [PrimaryKey, NotNull, Unique]
        public Guid Id { get; set; }

        [NotNull]
        public string ImageLoader { get; set; }
        [NotNull]
        public string ImageSource { get; set; }
    }
}
