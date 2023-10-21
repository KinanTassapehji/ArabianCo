using Abp.Application.Services.Dto;
using ArabianCo.Attachments.Dto;
using ArabianCo.Domain.Brands;
using System.Collections.Generic;

namespace ArabianCo.Brands.Dto;

public class BrandDto:EntityDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<BrandTranslation> Translations { get; set; }
    public LiteAttachmentDto Photo { get; set; }
}
