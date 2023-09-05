using AutoMapper;
using ArabianCo.Categories.Dto;
using ArabianCo.Domain.Categories;

namespace ArabianCo.Categories.Mapper;

public class CategoryMapProfile:Profile
{
    public CategoryMapProfile()
    {
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();
        CreateMap<CategoryTranslationDto, CategoryTranslation>().ReverseMap();
    }
}
