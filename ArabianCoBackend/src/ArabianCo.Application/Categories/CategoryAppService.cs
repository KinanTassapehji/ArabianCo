using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using ArabianCo.Categories.Dto;
using ArabianCo.CrudAppServiceBase;
using ArabianCo.Domain.Attachments;
using ArabianCo.Domain.Categories;
using ArabianCo.Localization.SourceFiles;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArabianCo.Categories;

public class CategoryAppService : ArabianCoAsyncCrudAppService<Category, CategoryDetaisDto, int, LiteCategoryDto, PagedCategoryResultRequestDto, CreateCategoryDto, UpdateCategoryDto>, ICategoryAppService
{
    private readonly ICategoryManger _categoryManger;
    private readonly IAttachmentManager _attachmentManager;
    public CategoryAppService(IRepository<Category, int> repository, ICategoryManger categoryManger, IAttachmentManager attachmentManager) : base(repository)
    {
        _categoryManger = categoryManger;
        _attachmentManager = attachmentManager;
    }
    public override async Task<CategoryDetaisDto> CreateAsync(CreateCategoryDto input)
    {
        var transltion = ObjectMapper.Map<List<CategoryTranslation>>(input.Translations);
        if (await _categoryManger.CheckIfCategoryIsExist(transltion))
            throw new UserFriendlyException(string.Format(Exceptions.ObjectIsAlreadyExist, Tokens.Category));
        var entity = ObjectMapper.Map<Category>(input);
        var id = await _categoryManger.InsertAndGetIdAsync(entity);
        await _attachmentManager.CheckAndUpdateRefIdAsync(input.AttachmentId, Enums.Enum.AttachmentRefType.Category, id);
        return MapToEntityDto(entity);
    }
    public override async Task<PagedResultDto<LiteCategoryDto>> GetAllAsync(PagedCategoryResultRequestDto input)
    {
        var result = await base.GetAllAsync(input);
        foreach(var item in result.Items) 
        {
            var photo = await _attachmentManager.GetAttachmentByRefAsync(item.Id, Enums.Enum.AttachmentRefType.Category);
            if (photo != null)
            {
                item.Photo = new Attachments.Dto.LiteAttachmentDto
                {
                    Id = photo.Id,
                    Url = _attachmentManager.GetUrl(photo),
                    RefType = Enums.Enum.AttachmentRefType.Category
                };
            }
        }
        return result;
    }
    public override async Task<CategoryDetaisDto> GetAsync(EntityDto<int> input)
    {
        var entity = await _categoryManger.GetEntityByIdAsync(input.Id);
        var result = MapToEntityDto(entity);
        var photo = await _attachmentManager.GetAttachmentByRefAsync(entity.Id, Enums.Enum.AttachmentRefType.Category);
        if (photo != null)
        {
            result.Photo = new Attachments.Dto.LiteAttachmentDto
            {
                Id = photo.Id,
                Url = _attachmentManager.GetUrl(photo),
                RefType = Enums.Enum.AttachmentRefType.Category
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
        MapToEntity(input, category);
        await _categoryManger.UpdateAsync(category);
        return MapToEntityDto(category);
    }
    public override async Task DeleteAsync(EntityDto<int> input)
    {
        await _categoryManger.GetLiteEntityByIdAsync(input.Id);
        await _categoryManger.DeleteAsync(input.Id);
    }
    protected override IQueryable<Category> CreateFilteredQuery(PagedCategoryResultRequestDto input)
    {
        var data = base.CreateFilteredQuery(input);
        data = data.Include(x => x.Translations);
        return data;
    }
}
