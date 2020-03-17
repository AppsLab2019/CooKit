using CooKit.Models.Steps;
using Xamarin.Forms;

namespace CooKit.Models.Impl.SQLite.Steps
{
    internal sealed class SQLiteBigImageRecipeStep : SQLiteRecipeStep, IBigImageRecipeStep
    {
        public ImageSource Image { get; internal set; }

        public override object SpecificInternalInfo => _specificInternalInfo;
        private readonly SQLiteBigImageRecipeStepInfo _specificInternalInfo;

        internal SQLiteBigImageRecipeStep(SQLiteBigImageRecipeStepInfo specificInfo, SQLiteRecipeStepInfo info)
            : base(info) => _specificInternalInfo = specificInfo;
    }
}
