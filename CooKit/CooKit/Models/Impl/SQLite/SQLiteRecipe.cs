using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CooKit.Models.Impl.SQLite
{
    internal sealed class SQLiteRecipe : IRecipe, ISQLiteStorable<SQLiteRecipeInfo>
    {
        public Guid Id => InternalInfo.Id;
        public string Name => InternalInfo.Name;
        public string Description => InternalInfo.Description;
        public ImageSource MainImage { get; internal set; }

        public IReadOnlyList<IPictogram> Pictograms { get; internal set; }
        public IReadOnlyList<IIngredient> Ingredients { get; internal set; }
        public IReadOnlyList<string> Steps { get; internal set; }

        public SQLiteRecipeInfo InternalInfo { get; }

        internal SQLiteRecipe(SQLiteRecipeInfo info) =>
            InternalInfo = info;
    }
}
