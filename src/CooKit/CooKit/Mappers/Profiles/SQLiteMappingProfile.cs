using System;
using AutoMapper;
using CooKit.Mappers.Converters.Guids;
using CooKit.Mappers.Converters.Uris;
using CooKit.Models.Recipes;

namespace CooKit.Mappers.Profiles
{
    public sealed class SQLiteMappingProfile : Profile
    {
        private readonly IGuidListToStringConverter _guidToString;
        private readonly IStringToGuidListConverter _stringToGuid;

        private readonly IUriListToStringConverter _uriToString;
        private readonly IStringToUriListConverter _stringToUri;

        public SQLiteMappingProfile(IGuidListToStringConverter guidToString,
            IStringToGuidListConverter stringToGuid,
            IUriListToStringConverter uriToString,
            IStringToUriListConverter stringToUri)
        {
            if (guidToString is null)
                throw new ArgumentNullException(nameof(guidToString));

            if (stringToGuid is null)
                throw new ArgumentNullException(nameof(stringToGuid));

            if (uriToString is null)
                throw new ArgumentNullException(nameof(uriToString));

            if (stringToUri is null)
                throw new ArgumentNullException(nameof(stringToUri));

            _guidToString = guidToString;
            _stringToGuid = stringToGuid;

            _uriToString = uriToString;
            _stringToUri = stringToUri;

            MapRecipeToDto();
            MapDtoToRecipe();
        }

        private void MapRecipeToDto()
        {
            // TODO: finish map (converters for properties)
            CreateMap<IRecipe, SQLiteRecipeDto>()
                .ForMember(dest => dest.Pictograms, opt =>
                {
                    opt.ConvertUsing(_guidToString, src => src.PictogramIds);
                })
                .ForMember(dest => dest.Ingredients, opt =>
                {
                    opt.ConvertUsing(_guidToString, src => src.IngredientIds);
                });
        }

        private void MapDtoToRecipe()
        {
            // TODO: finish map (converters for properties)
            CreateMap<SQLiteRecipeDto, Recipe>()
                .ForMember(dest => dest.PictogramIds, opt =>
                {
                    opt.ConvertUsing(_stringToGuid, src => src.Pictograms);
                })
                .ForMember(dest => dest.IngredientIds, opt =>
                {
                    opt.ConvertUsing(_stringToGuid, src => src.Ingredients);
                });
            
            CreateMap<SQLiteRecipeDto, IRecipe>().As<Recipe>();
        }
    }
}
