using Abp.Application.Services.Dto;
using ArabianCo.Attachments.Dto;
using System.Collections.Generic;

namespace ArabianCo.AboutUss.Dto;

public class AboutUsDto : EntityDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public int? ShowOrder { get; set; }
    public LiteAttachmentDto Photo { get; set; }
    public List<AboutUsTranslationDto> Translations { get; set; }
}
