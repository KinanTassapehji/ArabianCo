using Abp.Authorization.Users;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ArabianCo.Domain.Questions;

public class Question:FullAuditedEntity
{
    [EmailAddress]
    [StringLength(AbpUserBase.MaxEmailAddressLength)]
    public string Email { get; set; }
    [Required]
    [StringLength(AbpUserBase.MaxNameLength)]
    public string FullName { get; set; }
    [Required]
    [StringLength(AbpUserBase.MaxPhoneNumberLength)]
    public string PhoneNumber { get; set; }
    [Required]
    [StringLength(200)]
    public string YourQuestion { get; set; }
    [DefaultValue(false)]
    public bool IsRead { get; set; }
}
