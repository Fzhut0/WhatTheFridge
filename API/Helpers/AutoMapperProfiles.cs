using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AppUser, UserDto>();
        CreateMap<RegisterDto, AppUser>();

        CreateMap<Recipe, RecipeDto>()
            .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.RecipeIngredients.Select(ri => ri.Ingredient)));
        
        CreateMap<RecipeDto, Recipe>()
    .ForMember(dest => dest.RecipeIngredients, opt => opt.MapFrom(src => src.Ingredients.Select(i => new RecipeIngredient 
    { 
        Ingredient = new Ingredient 
        { 
            Name = i.IngredientName 
        }, 
        IngredientQuantity = i.Quantity 
    })));

        CreateMap<RecipeIngredientDto, RecipeIngredient>();

        CreateMap<Ingredient, IngredientDto>();
        CreateMap<IngredientDto, Ingredient>();

        CreateMap<DateTime, DateTime>().ConvertUsing(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
        CreateMap<DateTime?, DateTime?>().ConvertUsing(d => d.HasValue ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null);
    }
}

}