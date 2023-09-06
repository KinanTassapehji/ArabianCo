using Abp.Application.Services.Dto;

namespace ArabianCo.MaintenanceRequests.Dto;

public class LiteMaintenanceRequestDto:EntityDto
{
    public string Email { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string SerialNumber { get; set; }
    public string Problem { get; set; }
    public bool IsInWarrantyPeriod { get; set; }
}
