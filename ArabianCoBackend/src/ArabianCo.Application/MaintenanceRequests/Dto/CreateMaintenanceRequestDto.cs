using Abp.Authorization.Users;
using System.ComponentModel.DataAnnotations;
using static ArabianCo.Enums.Enum;

namespace ArabianCo.MaintenanceRequests.Dto;

public class CreateMaintenanceRequestDto
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
    [Required]
    public int AreaId { get; set; }
    [Required]
    public int BrandId { get; set; }
    [Required]
    public int CategoryId { get; set; }
    public int? AttachmentId { get; set; }
}
