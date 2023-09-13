using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using ArabianCo.Brands.Dto;
using ArabianCo.CrudAppServiceBase;
using ArabianCo.Domain.Attachments;
using ArabianCo.Domain.Brands;
using ArabianCo.Localization.SourceFiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArabianCo.Brands;

public class BrandAppService : ArabianCoAsyncCrudAppService<Brand, BrandDto, int, LiteBrandDto, PagedBrandResultRequestDto, CreateBrandDto, UpdateBrandDto>, IBrandAppService
{
    private readonly IBrandManger _brandManger;
    private readonly IAttachmentManager _attachmentManager;
    public BrandAppService(IRepository<Brand, int> repository, IBrandManger brandManger, IAttachmentManager attachmentManager) : base(repository)
    {
        _brandManger = brandManger;
        _attachmentManager = attachmentManager;
    }
    public override async Task<PagedResultDto<LiteBrandDto>> GetAllAsync(PagedBrandResultRequestDto input)
    {
        var result = await base.GetAllAsync(input);
        foreach (var item in result.Items)
        {
            var photo = await _attachmentManager.GetAttachmentByRefAsync(item.Id, Enums.Enum.AttachmentRefType.Brand);
            if (photo != null)
            {
                item.Photo = new Attachments.Dto.LiteAttachmentDto
                {
                    Id = photo.Id,
                    Url = _attachmentManager.GetUrl(photo),
                    RefType = Enums.Enum.AttachmentRefType.Brand
                };
            }
        }
        return result;
    }
    public override async Task<BrandDto> CreateAsync(CreateBrandDto input)
    {
        var translation = ObjectMapper.Map<List<BrandTranslation>>(input.Translations);
        if (await _brandManger.CheckIfBrandIsExist(translation))
            throw new UserFriendlyException(string.Format(Exceptions.ObjectIsAlreadyExist));
        var entity = ObjectMapper.Map<Brand>(input);
        entity.IsActive = true;
        var id = await _brandManger.InsertAndGetIdAsync(entity);
        await _attachmentManager.CheckAndUpdateRefIdAsync(input.AttachmentId,Enums.Enum.AttachmentRefType.Brand,id);
        return MapToEntityDto(entity);
    }
    public override async Task<BrandDto> GetAsync(EntityDto<int> input)
    {
        var entity = await _brandManger.GetEntityByIdAsync(input.Id);
        var entityDto = MapToEntityDto(entity);
        var photo = await _attachmentManager.GetAttachmentByRefAsync(entity.Id, Enums.Enum.AttachmentRefType.Brand);
        if (photo != null)
        {
            entityDto.Photo = new Attachments.Dto.LiteAttachmentDto
            {
                Id = photo.Id,
                Url = _attachmentManager.GetUrl(photo),
                RefType = Enums.Enum.AttachmentRefType.Brand
            };
        }
        return entityDto;
    }
    public override async Task<BrandDto> UpdateAsync(UpdateBrandDto input)
    {
        var entity = await _brandManger.GetEntityByIdAsync(input.Id);
        if (entity == null)
            throw new UserFriendlyException(string.Format("Brand With Id {0} Not Found ",input.Id));
        var oldPhoto = await _attachmentManager.GetAttachmentByRefAsync(entity.Id, Enums.Enum.AttachmentRefType.Brand);
        if(oldPhoto != null)
        {
            if (oldPhoto.Id != input.AttachmentId)
            {
                await _attachmentManager.DeleteRefIdAsync(oldPhoto);
                await _attachmentManager.CheckAndUpdateRefIdAsync(input.AttachmentId, Enums.Enum.AttachmentRefType.Brand, input.Id);
            } 
        }
        entity.Translations.Clear();
        MapToEntity(input, entity);
        entity.LastModificationTime = DateTime.UtcNow;
        await _brandManger.UpdateAsync(entity);
        return MapToEntityDto(entity);
    }
    [AbpAuthorize]
    [HttpPut]
    public async Task SwitchActivation(SwitchActivationInputDto input)
    {
        var entity = await _brandManger.GetLiteEntityByIdAsync(input.Id);
        entity.IsActive = !entity.IsActive;
        entity.LastModificationTime = DateTime.UtcNow;
        entity.LastModifierUserId = AbpSession.UserId.Value;
        await _brandManger.UpdateAsync(entity);
    }
    protected override IQueryable<Brand> CreateFilteredQuery(PagedBrandResultRequestDto input)
    {
        var data = base.CreateFilteredQuery(input);
        if (!input.IsActive.HasValue)
            data = data.Where(x => x.IsActive == input.IsActive.Value);
        data = data.Include(x => x.Translations);
        return data;
    }
}
