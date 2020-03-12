using System;
using SQLite;

namespace CooKit.Models.Impl.SQLite
{
    public sealed class SQLiteRecipeInfo
    {
        [PrimaryKey, NotNull, Unique]
        public Guid Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public string Description { get; set; }
        
        [NotNull]
        public string ImageLoader { get; set; }
        [NotNull]
        public string ImageSource { get; set; }

        public string IngredientIds { get; set; }
        public string PictogramIds { get; set; }
        [NotNull]
        public string StepIds { get; set; }
    }
}
