using System.Threading.Tasks;
using Autofac;

namespace CooKit.Strategies.Initialization
{
    public interface IInitializationStrategy
    {
        Task Initialize(IContainer container);
    }
}
