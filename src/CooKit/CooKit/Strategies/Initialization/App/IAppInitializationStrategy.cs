using System.Threading.Tasks;
using Autofac;

namespace CooKit.Strategies.Initialization.App
{
    public interface IAppInitializationStrategy
    {
        Task InitializeApp(IContainer container);
    }
}
