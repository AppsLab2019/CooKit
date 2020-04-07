using System.Diagnostics;
using AutoMapper;
using CooKit.Mappers.Profiles;

namespace CooKit.Mappers
{
    public class MapperFactory : IMapperFactory
    {
        public IMapper CreateMapper()
        {
            var configuration = new MapperConfiguration(conf =>
            {
                conf.AddProfile<SQLiteMappingProfile>();
            });

            AssertConfigurationIfDebug(configuration);
            return configuration.CreateMapper();
        }
        
        [Conditional("DEBUG")]
        public static void AssertConfigurationIfDebug(MapperConfiguration configuration) => 
            configuration.AssertConfigurationIsValid();
    }
}
