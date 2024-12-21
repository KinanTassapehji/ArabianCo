using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace ArabianCo.Attributes.Dto;

public class AttributeDto : EntityDto
{
    public string Name { get; set; }
    public List<AttributeTranslationDto> Translations { get; set; }
    public List<IndexDto> Categories { get; set; }
}
