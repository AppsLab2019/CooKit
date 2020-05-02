using System.Threading.Tasks;

namespace CooKit.Services.Database
{
    public interface ISQLiteInitialization
    {
        Task InitializeAsync();
    }
}
