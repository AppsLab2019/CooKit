using CooKit.Models.Steps;

namespace CooKit.Models.Impl.SQLite.Steps
{
    internal sealed class SQLiteTextRecipeStep : SQLiteRecipeStep, ITextRecipeStep
    {
        public string Text => _specificInternalInfo.Text;

        public override object SpecificInternalInfo => _specificInternalInfo;
        private readonly SQLiteTextRecipeStepInfo _specificInternalInfo;

        internal SQLiteTextRecipeStep(SQLiteTextRecipeStepInfo specificInfo, SQLiteRecipeStepInfo info)
            : base(info) => _specificInternalInfo = specificInfo;
    }
}
