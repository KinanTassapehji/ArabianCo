using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using ArabianCo.AboutUss.Dto;
using ArabianCo.CrudAppServiceBase;
using ArabianCo.Domain.AboutUss;
using ArabianCo.Domain.Attachments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ArabianCo.AboutUss;

public class AboutUsAppService : ArabianCoAsyncCrudAppService<AboutUs, AboutUsDto, int, AboutUsDto, PagedAboutUsResultRequestDto, CreateAboutUsDto, UpdateAboutUsDto>, IAboutUsAppService
{
    private readonly IAboutUsManger _aboutUsManger;
    private readonly IAttachmentManager _attachmentManager;
    public AboutUsAppService(IRepository<AboutUs, int> repository, IAboutUsManger aboutUsManger, IAttachmentManager attachmentManager) : base(repository)
    {
        _aboutUsManger = aboutUsManger;
        _attachmentManager = attachmentManager;
    }
    [AbpAuthorize]
    public override async Task<AboutUsDto> CreateAsync(CreateAboutUsDto input)
    {
        var photo = await _attachmentManager.GetAndCheckAsync(input.AttachmentId, Enums.Enum.AttachmentRefType.AboutUs);
        var entity = await base.CreateAsync(input);
        await UnitOfWorkManager.Current.SaveChangesAsync();
        await _attachmentManager.UpdateRefIdAsync(photo, entity.Id);
        return entity;
    }
    public override async Task<PagedResultDto<AboutUsDto>> GetAllAsync(PagedAboutUsResultRequestDto input)
    {
        var result = await base.GetAllAsync(input);
        foreach (var item in result.Items)
        {
            var photo = await _attachmentManager.GetByRefAsync(item.Id, Enums.Enum.AttachmentRefType.AboutUs);
            if (photo != null)
            {
                item.Photo = new Attachments.Dto.LiteAttachmentDto
                {
                    Id = photo.Id,
                    RefType = photo.RefType,
                    Url = _attachmentManager.GetUrl(photo)
                };
            }
        }
        return result;
    }
    public override async Task<AboutUsDto> UpdateAsync(UpdateAboutUsDto input)
    {
        var entity = await _aboutUsManger.GetEntityByIdAsync(input.Id);
        entity.Translations.Clear();
        var photo = await _attachmentManager.GetByRefAsync(input.Id, Enums.Enum.AttachmentRefType.AboutUs);
        if (photo != null)
        {
            await _attachmentManager.DeleteRefIdAsync(photo);
        }
        await _attachmentManager.CheckAndUpdateRefIdAsync(input.AttachmentId, Enums.Enum.AttachmentRefType.AboutUs, input.Id);
        var entityDto = await base.UpdateAsync(input);
        return entityDto;
    }
    public override async Task<AboutUsDto> GetAsync(EntityDto<int> input)
    {
        var entityDto = MapToEntityDto(await _aboutUsManger.GetEntityByIdAsync(input.Id));
        var photo = await _attachmentManager.GetByRefAsync(input.Id, Enums.Enum.AttachmentRefType.AboutUs);
        if (photo != null)
        {
            entityDto.Photo = new Attachments.Dto.LiteAttachmentDto
            {
                Id = photo.Id,
                RefType = photo.RefType,
                Url = _attachmentManager.GetUrl(photo)
            };
        }
        return entityDto;
    }
    [AbpAuthorize]
    [HttpPut]
    public async Task SwitchActivation(SwitchActivationInputDto input)
    {
        var entity = await Repository.GetAsync(input.Id);
        if (entity != null)
        {
            entity.IsActive = !entity.IsActive;
            await Repository.UpdateAsync(entity);
        }
        else
        {
            throw new UserFriendlyException();
        }
    }
    protected override IQueryable<AboutUs> CreateFilteredQuery(PagedAboutUsResultRequestDto input)
    {
        var data = base.CreateFilteredQuery(input);
        data = data.Include(x => x.Translations.Where(t => !t.IsDeleted));
        if (input.IsActive.HasValue)
        {
            data = data.Where(x => x.IsActive == input.IsActive);
        }
        else
        {
            data = data.Where(x => x.IsActive);
        }
        return data;
    }
    protected override IQueryable<AboutUs> ApplySorting(IQueryable<AboutUs> query, PagedAboutUsResultRequestDto input)
    {
        var data = base.ApplySorting(query, input);
        data = data.OrderBy(x => x.ShowOrder);
        return data;
    }
}
