using AutoMapper;
using CooKit.Mobile.Contexts.Entities;
using CooKit.Mobile.Models;

namespace CooKit.Mobile.Profiles
{
    // TODO: implement this
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<RecipeEntity, Recipe>();
            CreateMap<Recipe, RecipeEntity>();
        }
    }
}
