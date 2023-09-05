using ArabianCo.Brands.Dto;
using ArabianCo.Domain.Brands;
using AutoMapper;

namespace ArabianCo.Brands.Mapper;

internal class BrandMapProfile:Profile
{
    public BrandMapProfile()
    {
        CreateMap<CreateBrandDto, Brand>();
        CreateMap<UpdateBrandDto, Brand>();
        CreateMap<BrandTranslationDto, BrandTranslation>();
    }
}
