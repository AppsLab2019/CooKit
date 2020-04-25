using Autofac;

namespace CooKit.Strategies
{
    public sealed class StrategyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(type => type.Name.EndsWith("Strategy"))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
