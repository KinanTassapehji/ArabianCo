using Abp.Application.Services.Dto;

namespace ArabianCo.MaintenanceRequests.Dto;

public class PagedMaintenanceRequestResultDto:PagedResultRequestDto
{
	public bool IsDeleted { get; set; } = false;
	public string phoneNumber { get; set; }
}
