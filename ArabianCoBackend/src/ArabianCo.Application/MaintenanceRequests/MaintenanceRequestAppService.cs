using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using ArabianCo.CrudAppServiceBase;
using ArabianCo.Domain.MaintenanceRequests;
using ArabianCo.MaintenanceRequests.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ArabianCo.MaintenanceRequests;

public class MaintenanceRequestAppService : ArabianCoAsyncCrudAppService<MaintenanceRequest, MaintenanceRequestDto, int, LiteMaintenanceRequestDto, PagedMaintenanceRequestResultDto, CreateMaintenanceRequestDto, UpdateMaintenanceRequestDto>, IMaintenanceRequestAppService
{
    public MaintenanceRequestAppService(IRepository<MaintenanceRequest, int> repository) : base(repository)
    {
    }
    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<MaintenanceRequestDto> UpdateAsync(UpdateMaintenanceRequestDto input)
    {
        return base.UpdateAsync(input);
    }
    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task DeleteAsync(EntityDto<int> input)
    {
        return base.DeleteAsync(input);
    }
}
