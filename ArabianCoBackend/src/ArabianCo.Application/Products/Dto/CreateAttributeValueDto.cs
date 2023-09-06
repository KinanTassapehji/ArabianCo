using ArabianCo.Domain.AttributeValues;
using System.Collections.Generic;

namespace ArabianCo.Products.Dto;

public class CreateAttributeValueDto
{
    public int AttributeId { get; set; }
    public List<AttributeValueTranslationDto> Translations { get; set; }
}
