using Abp.Application.Services.Dto;
using ArabianCo.Attachments.Dto;

namespace ArabianCo.Brands.Dto;

public class BrandDto:EntityDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public LiteAttachmentDto Photo { get; set; }
}
