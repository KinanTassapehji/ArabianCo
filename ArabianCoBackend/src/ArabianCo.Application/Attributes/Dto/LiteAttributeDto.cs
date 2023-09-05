using Abp.Application.Services.Dto;
using ArabianCo.Categories.Dto;
using System.Collections.Generic;

namespace ArabianCo.Attributes.Dto;

public class LiteAttributeDto:EntityDto
{
    public string Name { get; set; }
    public LiteCategoryDto Category { get; set; }
    public List<AttributeTranslationDto> Translations { get; set; }

}
