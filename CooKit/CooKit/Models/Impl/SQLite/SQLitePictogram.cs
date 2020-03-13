using System;
using Xamarin.Forms;

namespace CooKit.Models.Impl.SQLite
{
    internal sealed class SQLitePictogram : IPictogram, ISQLiteStorable<SQLitePictogramInfo>
    {
        public Guid Id => InternalInfo.Id;
        public string Name => InternalInfo.Name;
        public string Description => InternalInfo.Description;
        public ImageSource Image { get; internal set; }

        public SQLitePictogramInfo InternalInfo { get; }

        internal SQLitePictogram(SQLitePictogramInfo info) =>
            InternalInfo = info;
    }
}
