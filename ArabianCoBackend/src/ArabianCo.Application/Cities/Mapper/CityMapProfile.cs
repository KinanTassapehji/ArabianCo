using AutoMapper;
using ArabianCo.Cities.Dto;
using ArabianCo.Domain.Cities;

namespace ArabianCo.Cities.Mapper
{
    public class CityMapProfile : Profile
    {
        public CityMapProfile()
        {
            CreateMap<CreateCityDto, City>();
            CreateMap<CreateCityDto, CityDto>();
            CreateMap<CityDto, City>();
            CreateMap<UpdateCityDto, City>();

        }
    }
}
