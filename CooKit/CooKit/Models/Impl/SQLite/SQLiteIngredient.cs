using System;
using Xamarin.Forms;

namespace CooKit.Models.Impl.SQLite
{
    internal sealed class SQLiteIngredient : IIngredient
    {
        public Guid Id { get; internal set; }
        public string Name { get; internal set; }
        public ImageSource Icon { get; internal set; }
    }
}
