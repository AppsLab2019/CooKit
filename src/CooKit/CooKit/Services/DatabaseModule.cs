using Autofac;
using CooKit.Services.Factories;

namespace CooKit.Services
{
    public sealed class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SQLiteConnectionFactory>().As<ISQLiteConnectionFactory>().SingleInstance();
            builder.Register(ctx => ctx.Resolve<ISQLiteConnectionFactory>().CreateConnection()).SingleInstance();
        }
    }
}
