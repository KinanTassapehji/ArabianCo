using Abp.Application.Services.Dto;
using ArabianCo.Cities.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArabianCo.Areas.Dto
{
    public class LiteAreaDto : EntityDto<int>
    {

        [StringLength(500)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public LiteCityDto City { get; set; }
        public List<AreaTranslationDto> Translations { get; set; }
    }

}
