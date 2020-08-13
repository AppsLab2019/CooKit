using System.Threading.Tasks;

namespace CooKit.Mobile.Resources.Database
{
    public interface IDatabaseExtractor
    {
        Task ExtractDatabaseAsync(string destinationPath);
    }
}
