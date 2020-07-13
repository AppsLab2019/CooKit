using Autofac;
using CooKit.Services.Extractors;
using CooKit.Services.Messages;
using CooKit.Services.Stores;

namespace CooKit.Services
{
    public sealed class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(RepositoryStore<>))
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(type => type.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<MessageBroker>().As<IMessageBroker>().SingleInstance();
            builder.RegisterType<ResourceExtractor>().As<IResourceExtractor>().SingleInstance();
        }
    }
}
