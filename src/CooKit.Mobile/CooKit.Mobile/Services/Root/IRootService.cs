using System;
using System.Threading.Tasks;

namespace CooKit.Mobile.Services.Root
{
    public interface IRootService
    {
        Task SetRootAsync(Type viewmodelType);
    }
}
