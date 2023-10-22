using ArabianCo.AboutUss.Dto;
using ArabianCo.Domain.AboutUss;
using AutoMapper;

namespace ArabianCo.AboutUss.Mapper;

internal class AboutUsMApProfile:Profile
{
    public AboutUsMApProfile()
    {
        CreateMap<CreateAboutUsDto, AboutUs>();
        CreateMap<UpdateAboutUsDto, AboutUs>();
    }
}
