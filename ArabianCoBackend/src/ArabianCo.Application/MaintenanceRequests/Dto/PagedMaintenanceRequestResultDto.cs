using Abp.Application.Services.Dto;

namespace ArabianCo.MaintenanceRequests.Dto;

public class PagedMaintenanceRequestResultDto:PagedResultRequestDto
{
	public string phoneNumber { get; set; }
}
