using AutoMapper;
using ArabianCo.Countries.Dto;
using ArabianCo.Domain.Countries;

namespace ArabianCo.Countries.Mapper
{
    public class CountryMapProfile : Profile
    {
        public CountryMapProfile()
        {
            CreateMap<CreateCountryDto, Country>();
            CreateMap<CreateCountryDto, CountryDto>();
            CreateMap<CountryDto, Country>();
            CreateMap<Country, UpdateCountryDto>();
            CreateMap<UpdateCountryDto, Country>();
        }
    }
}
