using Abp.Authorization.Users;
using Abp.Domain.Entities.Auditing;
using ArabianCo.Domain.Areas;
using ArabianCo.Domain.Brands;
using ArabianCo.Domain.Categories;
using ArabianCo.Domain.Cities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ArabianCo.Enums.Enum;

namespace ArabianCo.Domain.MaintenanceRequests;

public class MaintenanceRequest : FullAuditedEntity
{
    [Required]
    [EmailAddress]
    [StringLength(AbpUserBase.MaxEmailAddressLength)]
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
    public string Address { get; set; }
    public MaintenanceRequestsStatus Status { get; set; }
    [Required]
    public bool IsInWarrantyPeriod { get; set; }

    public int? AreaId { get; set; }
    [ForeignKey(nameof(AreaId))]
    public virtual Area Area { get; set; }
    [Required]
    public int BrandId { get; set; }
    [ForeignKey(nameof(BrandId))]
    public virtual Brand Brand { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; }

    public string OtherArea { get; set; }
    public string OtherCity { get; set; }

    public int? CityId { get; set; }

    [ForeignKey(nameof(CityId))]
    public virtual City City { get; set; }
}
