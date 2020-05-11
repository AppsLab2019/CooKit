using Autofac;

namespace CooKit.Factories
{
    public sealed class FactoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PageFactory>().As<IPageFactory>().SingleInstance();
        }
    }
}
