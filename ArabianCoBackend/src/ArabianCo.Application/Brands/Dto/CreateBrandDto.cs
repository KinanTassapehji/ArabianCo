using Abp.Runtime.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArabianCo.Brands.Dto;

public class CreateBrandDto : ICustomValidate
{
    public List<BrandTranslationDto> Translations { get; set; }
    public int AttachmentId { get; set; }
    public virtual void AddValidationErrors(CustomValidationContext context)
    {
        if (Translations is null || Translations.Count < 2)
            context.Results.Add(new ValidationResult("Translations must contain at least two elements"));
    }
}
