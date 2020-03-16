using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Models.Impl.SQLite.Steps;
using CooKit.Models.Impl.Steps;
using CooKit.Models.Steps;
using SQLite;

namespace CooKit.Services.Impl.SQLite
{
    internal sealed class SQLiteRecipeStepStore : SQLiteStoreBase<IRecipeStep, IRecipeStepBuilder, SQLiteRecipeStep, SQLiteRecipeStepInfo>, IRecipeStepStore
    {
        private Dictionary<Guid, SQLiteTextRecipeStepInfo> _idToTextInfo;

        internal SQLiteRecipeStepStore(SQLiteAsyncConnection connection) : base(connection) { }

        public override IRecipeStepBuilder CreateBuilder() =>
            new StoreCallbackRecipeStepBuilder(this);

        private protected override async Task PreInitAsync()
        {
            await Connection.CreateTableAsync<SQLiteTextRecipeStepInfo>();

            var textInfos = await Connection
                .Table<SQLiteTextRecipeStepInfo>()
                .ToArrayAsync();

            _idToTextInfo = textInfos.ToDictionary(info => info.Id);
        }

        private protected override Task RemoveObjectFromDatabase(SQLiteRecipeStep storable)
        {
            var baseTask = base.RemoveObjectFromDatabase(storable);
            var task = Connection.DeleteAsync(storable.SpecificInternalInfo);

            return Task.WhenAll(baseTask, task);
        }

        private protected override async Task<SQLiteRecipeStepInfo> CreateInfoFromBuilder(IRecipeStepBuilder builder)
        {
            RecipeStepType type;

            switch (builder)
            {
                case ITextRecipeStepBuilder specified:
                    await BuilderToTextInfo(specified);
                    type = RecipeStepType.TextOnly;
                    break;

                default:
                    return null;
            }

            return new SQLiteRecipeStepInfo
            {
                Id = builder.Id.Value,
                Type = type
            };
        }

        private protected override Task<SQLiteRecipeStep> CreateObjectFromInfo(SQLiteRecipeStepInfo info)
        {
            var step = info.Type switch
            {
                RecipeStepType.TextOnly => GenericInfoToTextStep(info),
                _ => null
            };

            return Task.FromResult(step);
        }

        #region Generic Info to Implementation

        private SQLiteRecipeStep GenericInfoToTextStep(SQLiteRecipeStepInfo info) =>
            _idToTextInfo.ContainsKey(info.Id) ? new SQLiteTextRecipeStep(_idToTextInfo[info.Id], info) : null;

        #endregion

        #region Builder to Specific Info

        private Task<SQLiteTextRecipeStepInfo> BuilderToTextInfo(ITextRecipeStepBuilder builder)
        {
            var info = new SQLiteTextRecipeStepInfo
            {
                Id = builder.Id.Value,
                Text = builder.Text.Value
            };

            return Connection
                .InsertAsync(info)
                .ContinueWith(_ => _idToTextInfo[info.Id] = info);
        }

        #endregion
    }
}
