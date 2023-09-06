using Abp.Runtime.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArabianCo.Products.Dto;

public class CreateProductDto : ICustomValidate
{
    public string ModelNumber { get; set; }
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    public List<ProductTranslationDto> Translations { get; set; }
    public List<CreateAttributeValueDto> AttributeValues { get; set; }
    public virtual void AddValidationErrors(CustomValidationContext context)
    {
        if (Translations is null || Translations.Count < 2)
            context.Results.Add(new ValidationResult("Translations must contain at least two elements"));
    }
}