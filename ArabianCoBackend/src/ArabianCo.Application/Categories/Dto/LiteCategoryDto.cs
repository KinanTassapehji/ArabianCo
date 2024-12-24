using Abp.Application.Services.Dto;
using ArabianCo.Attachments.Dto;
using System.Collections.Generic;

namespace ArabianCo.Categories.Dto;

public class LiteCategoryDto : EntityDto<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsParent { get; set; }
    public List<CategoryTranslationDto> Translations { get; set; }
    public LiteAttachmentDto Photo { get; set; }
    public LiteAttachmentDto Icon { get; set; }
}
