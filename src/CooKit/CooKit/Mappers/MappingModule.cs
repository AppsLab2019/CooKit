using Autofac;

namespace CooKit.Mappers
{
    public sealed class MappingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MapperFactory>().As<IMapperFactory>().SingleInstance();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(type => type.Namespace?.StartsWith("CooKit.Mappers.Converters") ?? false)
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(type => type.Namespace?.StartsWith("CooKit.Mappers.Profiles") ?? false)
                .AsSelf();

            builder.Register(ctx => ctx.Resolve<IMapperFactory>().CreateMapper(ctx)).SingleInstance();
        }
    }
}
