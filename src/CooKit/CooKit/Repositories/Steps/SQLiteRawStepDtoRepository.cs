using CooKit.Models.Steps;
using SQLite;

namespace CooKit.Repositories.Steps
{
    public sealed class SQLiteRawStepDtoRepository : SQLiteConcreteRepository<SQLiteRawStepDto>, ISQLiteRawStepDtoRepository
    {
        public SQLiteRawStepDtoRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }
    }
}
