using System.Collections.Generic;
using CooKit.Models;

namespace CooKit.Services
{
    public interface IPictogramStore
    {
        IReadOnlyList<IPictogram> LoadedPictograms { get; }
    }
}
