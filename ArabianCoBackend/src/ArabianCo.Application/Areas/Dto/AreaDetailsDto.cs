using Abp.Application.Services.Dto;
using ArabianCo.Cities.Dto;
using System;
using System.Collections.Generic;

namespace ArabianCo.Areas.Dto
{
    public class AreaDetailsDto : EntityDto
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }
        public LiteCityDto City { get; set; }
        public List<AreaTranslationDto> Translations { get; set; }
    }
}
