using Autofac;

namespace CooKit.Converters.Json
{
    public sealed class JsonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JsonConverter>().As<IJsonConverter>().SingleInstance();
        }
    }
}
