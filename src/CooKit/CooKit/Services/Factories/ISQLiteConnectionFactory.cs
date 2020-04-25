using SQLite;

namespace CooKit.Services.Factories
{
    public interface ISQLiteConnectionFactory
    {
        SQLiteAsyncConnection CreateConnection();
    }
}
