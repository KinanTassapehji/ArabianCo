using Abp.Application.Services.Dto;
using Abp.Domain.Entities;

namespace ArabianCo.MaintenanceRequests.Dto;

public class UpdateMaintenanceRequestDto:CreateMaintenanceRequestDto,IEntityDto
{
    public int Id { get; set; }
}
