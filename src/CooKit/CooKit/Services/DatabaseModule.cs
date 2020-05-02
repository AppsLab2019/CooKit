using Autofac;
using CooKit.Services.Database;

namespace CooKit.Services
{
    public sealed class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SQLiteConnectionFactory>().As<ISQLiteConnectionFactory>();
            builder.RegisterType<SQLiteInitialization>().As<ISQLiteInitialization>();
            builder.Register(ctx => ctx.Resolve<ISQLiteConnectionFactory>().CreateConnection()).SingleInstance();
        }
    }
}
