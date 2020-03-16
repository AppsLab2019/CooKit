using System;
using CooKit.Models.Steps;

namespace CooKit.Models.Impl.SQLite.Steps
{
    internal abstract class SQLiteRecipeStep : IRecipeStep, ISQLiteStorable<SQLiteRecipeStepInfo>
    {
        public Guid Id => InternalInfo.Id;
        public RecipeStepType Type => InternalInfo.Type;

        public SQLiteRecipeStepInfo InternalInfo { get; }
        public abstract object SpecificInternalInfo { get; }

        internal SQLiteRecipeStep(SQLiteRecipeStepInfo info) =>
            InternalInfo = info;
    }
}
