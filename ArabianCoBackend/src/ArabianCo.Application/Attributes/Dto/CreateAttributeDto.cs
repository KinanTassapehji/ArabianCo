using Abp.Collections.Extensions;
using Abp.Runtime.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArabianCo.Attributes.Dto;

public class CreateAttributeDto : ICustomValidate
{
    public List<int> CategoryIds { get; set; }
    public List<AttributeTranslationDto> Translations { get; set; }

    public void AddValidationErrors(CustomValidationContext context)
    {
        if (Translations is null || Translations.Count < 2)
            context.Results.Add(new ValidationResult("Translations must contain at least two elements"));
        if (CategoryIds.IsNullOrEmpty())
            context.Results.Add(new ValidationResult("CategoryIds must contain at least one elements"));
    }
}
