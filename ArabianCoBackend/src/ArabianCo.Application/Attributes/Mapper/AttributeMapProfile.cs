using ArabianCo.Attributes.Dto;
using ArabianCo.Domain.Attributes;
using AutoMapper;

namespace ArabianCo.Attributes.Mapper;

internal class AttributeMapProfile:Profile
{
    public AttributeMapProfile()
    {
        CreateMap<CreateAttributeDto, Attribute>();
        CreateMap<UpdateAttributeDto, Attribute>();
        CreateMap<Attribute, AttributeDto>();
        CreateMap<AttributeTranslationDto, AttributeTranslation>().ReverseMap();
    }
}
