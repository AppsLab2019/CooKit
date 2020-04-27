using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Autofac;
using AutoMapper;

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
                var profiles = ResolveProfilesFromExecutingAssembly(ctx);
                conf.AddProfiles(profiles);
            });

            AssertConfigurationIfDebug(configuration);
            return configuration.CreateMapper();
        }

        private static IEnumerable<Profile> ResolveProfilesFromExecutingAssembly(IComponentContext ctx)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var profileTypes = GetProfileTypesFromAssembly(assembly);
            return profileTypes.Select(ctx.Resolve).Cast<Profile>();
        }

        private static IEnumerable<Type> GetProfileTypesFromAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes();
            return types.Where(type => type.IsAssignableFrom(typeof(Profile)));
        }
        
        [Conditional("DEBUG")]
        public static void AssertConfigurationIfDebug(MapperConfiguration configuration) => 
            configuration.AssertConfigurationIsValid();
    }
}
