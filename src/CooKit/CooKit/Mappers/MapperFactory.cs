using System;
using System.Diagnostics;
using Autofac;
using AutoMapper;
using CooKit.Mappers.Profiles;

namespace CooKit.Mappers
{
    public class MapperFactory : IMapperFactory
    {
        public IMapper CreateMapper(IComponentContext ctx)
        {
            if (ctx is null)
                throw new ArgumentNullException(nameof(ctx));

            var configuration = new MapperConfiguration(conf =>
            {
                conf.ConstructServicesUsing(ctx.Resolve);

                // TODO: register using reflection
                conf.AddProfile(ctx.Resolve<SQLiteRecipeProfile>());
                conf.AddProfile(ctx.Resolve<SQLiteIngredientProfile>());
            });

            AssertConfigurationIfDebug(configuration);
            return configuration.CreateMapper();
        }
        
        [Conditional("DEBUG")]
        public static void AssertConfigurationIfDebug(MapperConfiguration configuration) => 
            configuration.AssertConfigurationIsValid();
    }
}
