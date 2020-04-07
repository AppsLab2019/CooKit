using AutoMapper;
using CooKit.Models;

namespace CooKit.Mappers.Profiles
{
    public sealed class SQLiteMappingProfile : Profile
    {
        public SQLiteMappingProfile()
        {
            MapRecipeToDto();
            MapDtoToRecipe();
        }

        private void MapRecipeToDto()
        {
            // TODO: finish map (converters for properties)
            CreateMap<Recipe, SQLiteRecipeDto>();
        }

        private void MapDtoToRecipe()
        {
            // TODO: finish map (converters for properties)
            CreateMap<SQLiteRecipeDto, Recipe>();
        }
    }
}
