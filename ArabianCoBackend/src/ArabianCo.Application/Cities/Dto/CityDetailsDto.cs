using Abp.Application.Services.Dto;
using ArabianCo.Areas.Dto;
using ArabianCo.Countries.Dto;
using System;
using System.Collections.Generic;

namespace ArabianCo.Cities.Dto
{
    public class CityDetailsDto : EntityDto
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }
        public CountryDto Country { get; set; }
        public List<LiteAreaDto> Areas { get; set; }
        public List<CityTranslationDto> Translations { get; set; }

    }
}
