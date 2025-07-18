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
    [MaxLength(10)]
    [MinLength(10)]
    public string PhoneNumber { get; set; }
    public string SerialNumber { get; set; }
    public string ModelNumber { get; set; }
    public string Problem { get; set; }
    public MaintenanceRequestsStatus Status { get; set; }
    [Required]
    public bool IsInWarrantyPeriod { get; set; }

    public string OtherArea { get; set; }
    public string OtherCity { get; set; }
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
		// Validate Area: require either AreaId or OtherArea
		bool hasValidArea = AreaId.HasValue || !OtherArea.IsNullOrWhiteSpace();
		if (!hasValidArea)
		{
			yield return new ValidationResult(
				"Area is Required",
				new[] { nameof(AreaId), nameof(OtherArea) }
			);
		}

		// Validate City: require either CityId or OtherCity
		bool hasValidCity = CityId.HasValue || !OtherCity.IsNullOrWhiteSpace();
		if (!hasValidCity)
		{
			yield return new ValidationResult(
				"City is Required",
				new[] { nameof(CityId), nameof(OtherCity) }
			);
		}
	}

}
