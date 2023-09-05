using Abp.Application.Services.Dto;
using ArabianCo.Countries.Dto;
using System.Collections.Generic;

namespace ArabianCo.Cities.Dto
{
    public class LiteCityDto : EntityDto<int>
    {

        public List<CityTranslationDto> Translations { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public bool IsActive { get; set; }
        public CountryDto Country { get; set; }
    }

}
