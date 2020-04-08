using System;
using System.Diagnostics;
using AutoMapper;
using CooKit.Mappers.Profiles;

namespace CooKit.Mappers
{
    public class MapperFactory : IMapperFactory
    {
        private readonly SQLiteMappingProfile _sqLiteMappingProfile;

        public MapperFactory(SQLiteMappingProfile sqLiteMapping)
        {
            if (sqLiteMapping is null)
                throw new ArgumentNullException(nameof(sqLiteMapping));

            _sqLiteMappingProfile = sqLiteMapping;
        }

        public IMapper CreateMapper()
        {
            var configuration = new MapperConfiguration(conf =>
            {
                conf.AddProfile(_sqLiteMappingProfile);
            });

            AssertConfigurationIfDebug(configuration);
            return configuration.CreateMapper();
        }
        
        [Conditional("DEBUG")]
        public static void AssertConfigurationIfDebug(MapperConfiguration configuration) => 
            configuration.AssertConfigurationIsValid();
    }
}
