using System;
using SQLite;

namespace CooKit.Models.Impl.SQLite
{
    public class SQLitePictogramInfo
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
    }
}
