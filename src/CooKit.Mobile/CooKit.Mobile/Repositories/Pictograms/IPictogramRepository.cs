using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Mobile.Models;

namespace CooKit.Mobile.Repositories.Pictograms
{
    public interface IPictogramRepository
    {
        ValueTask<Pictogram> GetPictogramAsync(int id);
        ValueTask<IList<Pictogram>> GetAllPictogramsAsync();
    }
}
