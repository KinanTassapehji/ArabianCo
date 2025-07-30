using ArabianCo.CrudAppServiceBase;
using ArabianCo.MaintenanceRequests.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArabianCo.MaintenanceRequests;

public interface IMaintenanceRequestAppService:IArabianCoAsyncCrudAppService<MaintenanceRequestDto,int,LiteMaintenanceRequestDto,PagedMaintenanceRequestResultDto,CreateMaintenanceRequestDto,UpdateMaintenanceRequestDto>
{
    Task<List<LiteMaintenanceRequestDto>> GetDeletedByPhoneNumberAsync(string phoneNumber);
}
