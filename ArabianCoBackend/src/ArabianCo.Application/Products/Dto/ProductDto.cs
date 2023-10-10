using Abp.Application.Services.Dto;
using ArabianCo.Attachments.Dto;
using System.Collections.Generic;

namespace ArabianCo.Products.Dto;

public class ProductDto:EntityDto
{
    public string ModelNumber { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public bool IsSpecial { get; set; }
    public IndexDto Brand { get; set; }
    public IndexDto Category { get; set; }
    public List<ProductCoverDto> Covers { get; set; }
    public List<LiteAttachmentDto> Photos { get; set; }
    public List<ProductTranslationDto> Translations { get; set; }
    public List<AttributValueDto> AttributeValues { get; set; }
}
