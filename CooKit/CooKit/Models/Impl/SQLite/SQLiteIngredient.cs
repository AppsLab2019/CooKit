using System;
using Xamarin.Forms;

namespace CooKit.Models.Impl.SQLite
{
    internal sealed class SQLiteIngredient : ISQLiteStorable<SQLiteIngredientInfo>, IIngredient
    {
        public Guid Id => InternalInfo.Id;
        public string Name => InternalInfo.Name;
        public ImageSource Icon { get; internal set; }

        public SQLiteIngredientInfo InternalInfo { get; }

        internal SQLiteIngredient(SQLiteIngredientInfo info) =>
            InternalInfo = info;
    }
}
