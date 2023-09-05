using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System.ComponentModel.DataAnnotations;

namespace ArabianCo.Brands.Dto;

public class UpdateBrandDto:CreateBrandDto, IEntityDto, ICustomValidate
{
    public int Id { get; set; }
    public virtual void AddValidationErrors(CustomValidationContext context)
    {
        if (Translations is null || Translations.Count < 2)
            context.Results.Add(new ValidationResult("Translations must contain at least two elements"));
    }
}
