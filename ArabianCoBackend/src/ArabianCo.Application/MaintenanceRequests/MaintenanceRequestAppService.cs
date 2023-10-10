using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using ArabianCo.CrudAppServiceBase;
using ArabianCo.Domain.Attachments;
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
    private readonly IAttachmentManager _attachmentManager;
    public MaintenanceRequestAppService(IRepository<MaintenanceRequest, int> repository /*IMaintenanceRequestsManger maintenanceRequestsManger*/, IAttachmentManager attachmentManager) : base(repository)
    {
        _attachmentManager = attachmentManager;
        /*_maintenanceRequestsManger = maintenanceRequestsManger;*/
    }
    public async override Task<MaintenanceRequestDto> CreateAsync(CreateMaintenanceRequestDto input)
    {
        var result = await base.CreateAsync(input);
        await CurrentUnitOfWork.SaveChangesAsync();
        if (input.AttachmentId.HasValue)
            await _attachmentManager.CheckAndUpdateRefIdAsync(input.AttachmentId.Value, Enums.Enum.AttachmentRefType.MaintenanceRequests, result.Id);
        return result;
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
        var attachment = await _attachmentManager.GetAttachmentByRefAsync(entity.Id, Enums.Enum.AttachmentRefType.MaintenanceRequests);
        var result = MapToEntityDto(entity);
        if (attachment != null)
            result.Attachment = new Attachments.Dto.LiteAttachmentDto
            {
                Id = attachment.Id,
                Url = _attachmentManager.GetUrl(attachment),
                RefType = Enums.Enum.AttachmentRefType.MaintenanceRequests

            };
        return result;
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
