using ArabianCo.Domain.AttributeValues;
using ArabianCo.Domain.Products;
using ArabianCo.Products.Dto;
using AutoMapper;

namespace ArabianCo.Products.Mapper;

internal class ProductMapProfile:Profile
{
    public ProductMapProfile()
    {
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        CreateMap<ProductTranslationDto, ProductTranslation>();
        CreateMap<CreateAttributeValueDto, AttributeValue>();
        CreateMap<AttributeValueTranslationDto, AttributeValueTranslation>().ReverseMap();
    }
}
