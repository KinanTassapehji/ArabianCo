using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using ArabianCo.CrudAppServiceBase;
using ArabianCo.Domain.MaintenanceRequests;
using ArabianCo.MaintenanceRequests.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ArabianCo.MaintenanceRequests;

public class MaintenanceRequestAppService : ArabianCoAsyncCrudAppService<MaintenanceRequest, MaintenanceRequestDto, int, LiteMaintenanceRequestDto, PagedMaintenanceRequestResultDto, CreateMaintenanceRequestDto, UpdateMaintenanceRequestDto>, IMaintenanceRequestAppService
{
    //private readonly IMaintenanceRequestsManger _maintenanceRequestsManger;
    public MaintenanceRequestAppService(IRepository<MaintenanceRequest, int> repository /*IMaintenanceRequestsManger maintenanceRequestsManger*/) : base(repository)
    {
        /*_maintenanceRequestsManger = maintenanceRequestsManger;*/
    }
    [AbpAuthorize]
    public override async Task<MaintenanceRequestDto> GetAsync(EntityDto<int> input)
    {
        var entity = await Repository.GetAll().Where(x => x.Id == input.Id)
            .Include(x => x.Brand).ThenInclude(x=>x.Translations)
            .Include(x => x.Category).ThenInclude(x=>x.Translations)
            .Include(x => x.Area).ThenInclude(x => x.Translations)
            .Include(x => x.Area.City).ThenInclude(x => x.Translations)
            .Include(x => x.Area.City.Country).ThenInclude(x => x.Translations).FirstOrDefaultAsync();
        return MapToEntityDto(entity);
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
