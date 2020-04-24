using AutoMapper;
using CooKit.Models.Ingredients;

namespace CooKit.Mappers.Profiles
{
    public sealed class SQLiteIngredientProfile : Profile
    {
        public SQLiteIngredientProfile()
        {
            MapDtoToIngredient();
            MapIngredientToDto();
        }

        private void MapDtoToIngredient()
        {
            CreateMap<SQLiteIngredientDto, Ingredient>()
                .ForMember(dest => dest.Template, opt => opt.Ignore());

            CreateMap<SQLiteIngredientDto, IIngredient>().As<Ingredient>();
        }

        private void MapIngredientToDto()
        {
            CreateMap<IIngredient, SQLiteIngredientDto>()
                .ForMember(dest => dest.TemplateId, opt => opt.Ignore());
        }
    }
}
