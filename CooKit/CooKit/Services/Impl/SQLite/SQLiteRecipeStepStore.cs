using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Models.Impl.SQLite.Steps;
using CooKit.Models.Steps;
using SQLite;

namespace CooKit.Services.Impl.SQLite
{
    internal sealed class SQLiteRecipeStepStore : SQLiteStoreBase<IRecipeStep, IRecipeStepBuilder, SQLiteRecipeStep, SQLiteRecipeStepInfo>, IRecipeStepStore
    {
        private Dictionary<Guid, SQLiteTextRecipeStepInfo> _idToTextInfo;

        internal SQLiteRecipeStepStore(SQLiteAsyncConnection connection) : base(connection) { }

        public override IRecipeStepBuilder CreateBuilder() => 
            throw new NotImplementedException();

        #region Database Manipulation

        protected internal override async Task AddObjectToDatabase(SQLiteRecipeStep storable)
        {
            await base.AddObjectToDatabase(storable);
            await Connection.InsertAsync(storable.SpecificInternalInfo);
        }

        protected internal override async Task RemoveObjectFromDatabase(SQLiteRecipeStep storable)
        {
            await base.RemoveObjectFromDatabase(storable);
            await Connection.DeleteAsync(storable.SpecificInternalInfo);
        }

        #endregion

        #region Initialization Overrides

        protected internal override async Task PreInitAsync()
        {
            await Connection.CreateTableAsync<SQLiteTextRecipeStepInfo>();

            var textInfos = await Connection
                .Table<SQLiteTextRecipeStepInfo>()
                .ToArrayAsync();

            _idToTextInfo = textInfos.ToDictionary(info => info.Id);
        }

        protected internal override Task PostInitAsync()
        {
            _idToTextInfo = null;
            return Task.CompletedTask;
        }

        #endregion

        protected internal override Task<SQLiteRecipeStep> CreateObjectFromBuilder(IRecipeStepBuilder builder) => 
            throw new NotImplementedException();

        protected internal override Task<SQLiteRecipeStep> CreateObjectFromInfo(SQLiteRecipeStepInfo info)
        {
            SQLiteRecipeStep result = info.Type switch
            {
                RecipeStepType.TextOnly => GenericInfoToTextStep(info),
                _ => null
            };

            return Task.FromResult(result);
        }

        private SQLiteTextRecipeStep GenericInfoToTextStep(SQLiteRecipeStepInfo info) =>
            _idToTextInfo.TryGetValue(info.Id, out var specificInfo)
                ? new SQLiteTextRecipeStep(specificInfo, info) : null;
    }
}
