using Abp;
using Abp.Authorization.Users;
using Abp.Extensions;
using ArabianCo.Localization.SourceFiles;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static ArabianCo.Enums.Enum;

namespace ArabianCo.MaintenanceRequests.Dto;

public class CreateMaintenanceRequestDto : IValidatableObject, IShouldInitialize
{
    public string Email { get; set; }
    [Required]
    [StringLength(AbpUserBase.MaxNameLength)]
    public string FullName { get; set; }
    [Required]
    [StringLength(AbpUserBase.MaxPhoneNumberLength)]
    public string PhoneNumber { get; set; }
    public string SerialNumber { get; set; }
    public string ModelNumber { get; set; }
    public string Problem { get; set; }
    public MaintenanceRequestsStatus Status { get; set; }
    [Required]
    public bool IsInWarrantyPeriod { get; set; }

    public string OtherArea { get; set; }
    public int? AreaId { get; set; }
    public int? CityId { get; set; }

    [Required]
    public int BrandId { get; set; }
    [Required]
    public int CategoryId { get; set; }
    public int? AttachmentId { get; set; }

    public void Initialize()
    {
        if (AreaId.HasValue)
        {
            CityId = null;
            OtherArea = null;
        }
        else if(CityId.HasValue && !OtherArea.IsNullOrEmpty())
        {
            AreaId = null;
        }
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!AreaId.HasValue)
            if (!CityId.HasValue || OtherArea.IsNullOrEmpty())
                yield return new ValidationResult("Area is Required", new List<string> { nameof(OtherArea), nameof(AreaId) });
    }
}
