using Autofac;

namespace CooKit.Converters
{
    public sealed class ConverterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IngredientToFormattedStringConverter>();
        }
    }
}
