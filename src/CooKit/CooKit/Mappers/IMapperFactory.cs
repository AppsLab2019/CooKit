using Autofac;
using AutoMapper;

namespace CooKit.Mappers
{
    public interface IMapperFactory
    {
        IMapper CreateMapper(IComponentContext ctx);
    }
}
