using System.Collections.Generic;

namespace ArabianCo.AboutUss.Dto;

public class CreateAboutUsDto
{
    public int AttachmentId { get; set; }
    public bool IsActive { get; set; }
    public int? ShowOrder { get; set; }
    public List<AboutUsTranslationDto> Translations { get; set; }
}
