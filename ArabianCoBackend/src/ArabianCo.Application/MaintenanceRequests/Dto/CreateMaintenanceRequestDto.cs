using Abp;
using Abp.Authorization.Users;
using Abp.Extensions;
using ArabianCo.Localization.SourceFiles;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using static ArabianCo.Enums.Enum;

namespace ArabianCo.MaintenanceRequests.Dto
{
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
			else if (CityId.HasValue && !OtherArea.IsNullOrEmpty())
			{
				AreaId = null;
			}
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			// ✅ Validate Area: require either AreaId or OtherArea
			bool hasValidArea = AreaId.HasValue || !OtherArea.IsNullOrWhiteSpace();
			if (!hasValidArea)
			{
				yield return new ValidationResult(
					"Area is Required",
					new[] { nameof(AreaId), nameof(OtherArea) }
				);
			}

			// ✅ Validate City: require either CityId or OtherCity
			bool hasValidCity = CityId.HasValue || !OtherCity.IsNullOrWhiteSpace();
			if (!hasValidCity)
			{
				yield return new ValidationResult(
					"City is Required",
					new[] { nameof(CityId), nameof(OtherCity) }
				);
			}

			// ✅ Validate Phone Number
			if (!string.IsNullOrWhiteSpace(PhoneNumber))
			{
				// Regular expression: must start with "05" and be 10 digits long (English digits only)
				var phoneRegex = new Regex(@"^05\d{8}$");

				if (!phoneRegex.IsMatch(PhoneNumber))
				{
					yield return new ValidationResult(
						"Phone number must be 10 digits, start with '05', and contain only English numbers.",
						new[] { nameof(PhoneNumber) }
					);
				}
			}
			else
			{
				yield return new ValidationResult(
					"Phone number is required.",
					new[] { nameof(PhoneNumber) }
				);
			}
		}
	}
}
