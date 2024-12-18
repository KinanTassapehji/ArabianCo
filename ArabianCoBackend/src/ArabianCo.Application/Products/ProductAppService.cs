using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Abp.UI;
using ArabianCo.Attachments.Dto;
using ArabianCo.CrudAppServiceBase;
using ArabianCo.Domain.Attachments;
using ArabianCo.Domain.AttributeValues;
using ArabianCo.Domain.Brands;
using ArabianCo.Domain.Categories;
using ArabianCo.Domain.Products;
using ArabianCo.Products.Dto;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArabianCo.Products;

public class ProductAppService : ArabianCoAsyncCrudAppService<Product, ProductDto, int, LiteProductDto, PagedProductResultRequestDto, CreateProductDto, UpdateProductDto>, IProductAppService
{
    private readonly IProductManger _productManger;
    private readonly IAttachmentManager _attachmentManager;
    private readonly IBrandManger _brandManger;
    private readonly ICategoryManger _categoryManger;
    private readonly IRepository<AttributeValue> _attributeValuesrepository;
    public ProductAppService(IRepository<Product, int> repository, IProductManger productManger, IAttachmentManager attachmentManager, ICategoryManger categoryManger, IBrandManger brandManger, IRepository<AttributeValue> attributeValuesrepository) : base(repository)
    {
        _productManger = productManger;
        _attachmentManager = attachmentManager;
        _categoryManger = categoryManger;
        _brandManger = brandManger;
        _attributeValuesrepository = attributeValuesrepository;
    }
    [AbpAuthorize]
    public override async Task<ProductDto> CreateAsync(CreateProductDto input)
    {
        await _brandManger.GetLiteEntityByIdAsync(input.BrandId);
        var category = await _categoryManger.GetLiteEntityByIdAsync(input.CategoryId);
        if (category.IsParent)
        {
            throw new UserFriendlyException("Category Is Parent Category You Can Not Add Products Into it");
        }
        var entity = MapToEntity(input);
        var id = await _productManger.InsertAndGetIdAsync(entity);
        foreach (var cover in input.ProductCoverAttachments)
        {
            await _attachmentManager.CheckAndUpdateRefIdAsync(cover.AttachmentId, Enums.Enum.AttachmentRefType.ProductCover, id, cover.Color);
        }
        foreach (var photo in input.ProductPhotosIds)
        {
            await _attachmentManager.CheckAndUpdateRefIdAsync(photo, Enums.Enum.AttachmentRefType.Product, id);
        }
        return MapToEntityDto(entity);
    }
    [AbpAuthorize]
    public override async Task<ProductDto> UpdateAsync(UpdateProductDto input)
    {
        var entity = await _productManger.GetEntityById(input.Id);
        var category = await _categoryManger.GetLiteEntityByIdAsync(input.CategoryId);
        if (category.IsParent)
        {
            throw new UserFriendlyException("Category Is Parent Category You Can Not Add Products Into it");
        }
        entity.Translations.Clear();
        //entity.AttributeValues.Clear();
        _attributeValuesrepository.RemoveRange(entity.AttributeValues);
        await _attachmentManager.DeleteAllRefIdAsync(input.Id, Enums.Enum.AttachmentRefType.ProductCover);
        await _attachmentManager.DeleteAllRefIdAsync(input.Id, Enums.Enum.AttachmentRefType.Product);
        MapToEntity(input, entity);
        foreach (var cover in input.ProductCoverAttachments)
        {
            await _attachmentManager.CheckAndUpdateRefIdAsync(cover.AttachmentId, Enums.Enum.AttachmentRefType.ProductCover, input.Id, cover.Color);
        }
        foreach (var photo in input.ProductPhotosIds)
        {
            await _attachmentManager.CheckAndUpdateRefIdAsync(photo, Enums.Enum.AttachmentRefType.Product, input.Id);
        }
        return MapToEntityDto(entity);
    }
    [AbpAllowAnonymous]
    public override async Task<PagedResultDto<LiteProductDto>> GetAllAsync(PagedProductResultRequestDto input)
    {
        var result = await base.GetAllAsync(input);
        foreach (var item in result.Items)
        {
            var covers = new List<ProductCoverDto>();
            var attachments = await _attachmentManager.GetListByRefAsync(item.Id, Enums.Enum.AttachmentRefType.ProductCover);
            if (!attachments.IsNullOrEmpty())
            {
                foreach (var cover in attachments)
                    covers.Add(new ProductCoverDto { Id = cover.Id, Color = cover.Color, RefType = Enums.Enum.AttachmentRefType.ProductCover, Url = _attachmentManager.GetUrl(cover) });
            }
            item.Covers = covers;
        }
        return result;
    }
    [AbpAuthorize]
    public override async Task DeleteAsync(EntityDto<int> input)
    {
        var entity = await _productManger.GetEntityById(input.Id);
        entity.Translations.Clear();
        await _productManger.DeleteRangeAttributeValues(entity.AttributeValues);
        await CurrentUnitOfWork.SaveChangesAsync();
        await _attachmentManager.DeleteAllRefIdAsync(input.Id, Enums.Enum.AttachmentRefType.ProductCover);
        await _attachmentManager.DeleteAllRefIdAsync(input.Id, Enums.Enum.AttachmentRefType.Product);
        await base.DeleteAsync(input);

    }
    [AbpAuthorize]
    [HttpPut]
    public async Task SwitchActivation(SwitchActivationInputDto input)
    {
        var entity = await Repository.FirstOrDefaultAsync(x => x.Id == input.Id);
        entity.LastModificationTime = DateTime.UtcNow;
        entity.LastModifierUserId = AbpSession.UserId.Value;
        entity.IsActive = !entity.IsActive;
        await _productManger.UpdateAsync(entity);
    }
    [AbpAllowAnonymous]
    public override async Task<ProductDto> GetAsync(EntityDto<int> input)
    {
        var entity = await _productManger.GetEntityById(input.Id);
        var entityDto = MapToEntityDto(entity);
        var covers = new List<ProductCoverDto>();
        var attachmentsCover = await _attachmentManager.GetListByRefAsync(input.Id, Enums.Enum.AttachmentRefType.ProductCover);
        if (!attachmentsCover.IsNullOrEmpty())
        {
            foreach (var cover in attachmentsCover)
                covers.Add(new ProductCoverDto { Id = cover.Id, Color = cover.Color, RefType = Enums.Enum.AttachmentRefType.ProductCover, Url = _attachmentManager.GetUrl(cover) });
        }
        entityDto.Covers = covers;
        var photos = new List<LiteAttachmentDto>();
        var attachmentsPhotos = await _attachmentManager.GetListByRefAsync(input.Id, Enums.Enum.AttachmentRefType.Product);
        if (!attachmentsPhotos.IsNullOrEmpty())
        {
            foreach (var photo in attachmentsPhotos)
                photos.Add(new LiteAttachmentDto { Id = photo.Id, RefType = Enums.Enum.AttachmentRefType.ProductCover, Url = _attachmentManager.GetUrl(photo) });
        }
        entityDto.Photos = photos;
        return entityDto;
    }
    protected override IQueryable<Product> CreateFilteredQuery(PagedProductResultRequestDto input)
    {
        var data = base.CreateFilteredQuery(input);
        if (!input.BrandIds.IsNullOrEmpty())
            data = data.Where(x => input.BrandIds.Contains(x.BrandId));
        if (!input.CategoryIds.IsNullOrEmpty())
            data = data.Where(x => input.CategoryIds.Contains(x.CategoryId));
        if (input.IsActive.HasValue)
            data = data.Where(x => x.IsActive == input.IsActive.Value);
        if (input.IsSpecial.HasValue)
            data = data.Where(x => x.IsSpecial == input.IsSpecial);
        data = data.Include(x => x.Translations);
        data = data.Include(c => c.Category.Translations).IgnoreQueryFilters();
        data = data.Include(x => x.Brand.Translations).IgnoreQueryFilters();
        if (!input.Keyword.IsNullOrEmpty())
        {
            data = data.Where(x => x.Translations.Any(p => p.Description.Contains(input.Keyword)) || x.Brand.Translations.Any(b => b.Name == input.Keyword) || x.Category.Translations.Any(x => x.Name == input.Keyword));
        }
        return data;
    }
}
