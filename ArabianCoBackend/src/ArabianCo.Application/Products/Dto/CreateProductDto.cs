using Abp.Runtime.Validation;
using Castle.Core.Internal;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ArabianCo.Products.Dto;

public class CreateProductDto : ICustomValidate
{
    public string ModelNumber { get; set; }
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    [DefaultValue(false)]
    public bool IsSpecial { get; set; }
    public List<ProductTranslationDto> Translations { get; set; }
    public List<CreateAttributeValueDto> AttributeValues { get; set; }
    public List<CreateProductCoverDto> ProductCoverAttachments { get; set; }
    public List<int> ProductPhotosIds { get; set; }
    public virtual void AddValidationErrors(CustomValidationContext context)
    {
        if (Translations is null || Translations.Count < 2)
            context.Results.Add(new ValidationResult("Translations must contain at least two elements"));
        if (ProductCoverAttachments.IsNullOrEmpty())
            context.Results.Add(new ValidationResult("Product cover must contain at least one element"));
    }
}