using System;
using Xamarin.Forms;

namespace CooKit.Models.Impl.SQLite
{
    internal sealed class SQLitePictogram : IPictogram
    {
        public Guid Id { get; internal set; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public ImageSource Image { get; internal set; }
    }
} 