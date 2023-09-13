using System.Collections.Generic;

namespace ArabianCo.Products.Dto;

public class AttributValueDto
{
    public IndexDto Attribute { get; set; }
    public string Value { get; set; }
    public List<AttributeValueTranslationDto> Translations { get; set; }

}
