using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using ArabianCo.Categories.Dto;
using ArabianCo.CrudAppServiceBase;
using ArabianCo.Domain.Attachments;
using ArabianCo.Domain.Categories;
using ArabianCo.Localization.SourceFiles;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArabianCo.Categories;

public class CategoryAppService : ArabianCoAsyncCrudAppService<Category, CategoryDetaisDto, int, LiteCategoryDto, PagedCategoryResultRequestDto, CreateCategoryDto, UpdateCategoryDto>, ICategoryAppService
{
    private readonly ICategoryManger _categoryManger;
    private readonly IAttachmentManager _attachmentManager;
    private readonly IMapper _mapper;
    public CategoryAppService(IRepository<Category, int> repository, ICategoryManger categoryManger, IAttachmentManager attachmentManager, IMapper mapper) : base(repository)
    {
        _categoryManger = categoryManger;
        _attachmentManager = attachmentManager;
        _mapper = mapper;
    }
    public override async Task<CategoryDetaisDto> CreateAsync(CreateCategoryDto input)
    {
        //var transltion = ObjectMapper.Map<List<CategoryTranslation>>(input.Translations);
        /* if (await _categoryManger.CheckIfCategoryIsExist(transltion))
             throw new UserFriendlyException(string.Format(Exceptions.ObjectIsAlreadyExist, Tokens.Category));*/
        var entity = ObjectMapper.Map<Category>(input);
        var id = await _categoryManger.InsertAndGetIdAsync(entity);
        if (input.AttachmentId.HasValue)
            await _attachmentManager.CheckAndUpdateRefIdAsync(input.AttachmentId.Value, Enums.Enum.AttachmentRefType.Category, id);
        if (input.IconId.HasValue)
            await _attachmentManager.CheckAndUpdateRefIdAsync(input.IconId.Value, Enums.Enum.AttachmentRefType.CategoryIcon, id);
        return MapToEntityDto(entity);
    }
    public override async Task<PagedResultDto<LiteCategoryDto>> GetAllAsync(PagedCategoryResultRequestDto input)
    {
        var result = await base.GetAllAsync(input);
        foreach (var item in result.Items)
        {
            var photo = await _attachmentManager.GetAttachmentByRefAsync(item.Id, Enums.Enum.AttachmentRefType.Category);
            var icon = await _attachmentManager.GetAttachmentByRefAsync(item.Id, Enums.Enum.AttachmentRefType.CategoryIcon);
            if (photo != null)
            {
                item.Photo = new Attachments.Dto.LiteAttachmentDto
                {
                    Id = photo.Id,
                    Url = _attachmentManager.GetUrl(photo),
                    RefType = Enums.Enum.AttachmentRefType.Category
                };
            }
            if (icon != null)
            {
                item.Icon = new Attachments.Dto.LiteAttachmentDto
                {
                    Id = icon.Id,
                    Url = _attachmentManager.GetUrl(icon),
                    RefType = Enums.Enum.AttachmentRefType.CategoryIcon
                };
            }
        }
        return result;
    }
    public override async Task<CategoryDetaisDto> GetAsync(EntityDto<int> input)
    {
        var entity = await _categoryManger.GetEntityByIdAsync(input.Id);
        var result = MapToEntityDto(entity);
        if (result.IsParent)
        {
            result.SubCategories = _mapper.Map<List<LiteCategoryDto>>(await _categoryManger.GetSubCategoriesByParentCategoryId(input.Id));
        }
        var photo = await _attachmentManager.GetAttachmentByRefAsync(entity.Id, Enums.Enum.AttachmentRefType.Category);
        var icon = await _attachmentManager.GetAttachmentByRefAsync(entity.Id, Enums.Enum.AttachmentRefType.CategoryIcon);
        if (photo != null)
        {
            result.Photo = new Attachments.Dto.LiteAttachmentDto
            {
                Id = photo.Id,
                Url = _attachmentManager.GetUrl(photo),
                RefType = Enums.Enum.AttachmentRefType.Category
            };
        }
        if (icon != null)
        {
            result.Icon = new Attachments.Dto.LiteAttachmentDto
            {
                Id = icon.Id,
                Url = _attachmentManager.GetUrl(icon),
                RefType = Enums.Enum.AttachmentRefType.CategoryIcon
            };
        }
        return result;
    }
    public override async Task<CategoryDetaisDto> UpdateAsync(UpdateCategoryDto input)
    {
        var category = await _categoryManger.GetEntityByIdAsync(input.Id);
        if (category == null)
            throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Category));
        category.Translations.Clear();
        var photo = await _attachmentManager.GetByRefAsync(input.Id, Enums.Enum.AttachmentRefType.Category);
        var icon = await _attachmentManager.GetByRefAsync(input.Id, Enums.Enum.AttachmentRefType.CategoryIcon);
        if (photo != null)
        {
            await _attachmentManager.DeleteRefIdAsync(photo);
        }
        if (icon != null)
        {
            await _attachmentManager.DeleteRefIdAsync(icon);
        }
        if (input.AttachmentId.HasValue)
            await _attachmentManager.CheckAndUpdateRefIdAsync(input.AttachmentId.Value, Enums.Enum.AttachmentRefType.Category, input.Id);
        if (input.IconId.HasValue)
            await _attachmentManager.CheckAndUpdateRefIdAsync(input.IconId.Value, Enums.Enum.AttachmentRefType.CategoryIcon, input.Id);
        MapToEntity(input, category);
        await _categoryManger.UpdateAsync(category);
        return MapToEntityDto(category);
    }
    public override async Task DeleteAsync(EntityDto<int> input)
    {
        await _categoryManger.GetEntityByIdAsync(input.Id);
        await _categoryManger.DeleteAsync(input.Id);
    }
    protected override IQueryable<Category> CreateFilteredQuery(PagedCategoryResultRequestDto input)
    {
        var data = base.CreateFilteredQuery(input);
        if (input.IsParent is not null)
            data = data.Where(x => x.IsParent == input.IsParent);
        if (input.ParentCategoryId is not null)
            data = data.Where(x => x.ParentCategoryId == input.ParentCategoryId);
        data = data.Include(x => x.Translations);
        return data;
    }
}
