using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using ArabianCo.Attributes.Dto;
using ArabianCo.CrudAppServiceBase;
using ArabianCo.Domain.Attributes;
using ArabianCo.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArabianCo.Attributes;

public class AttributeAppService : ArabianCoAsyncCrudAppService<Attribute, AttributeDto, int, LiteAttributeDto, PagedAttributeResultRequest, CreateAttributeDto, UpdateAttributeDto>, IAttributeAppService
{
    private readonly ICategoryManger _categoryManger;
    private readonly IAttributeManger _attributeManger;
    public AttributeAppService(IRepository<Attribute, int> repository, ICategoryManger categoryManger, IAttributeManger attributeManger) : base(repository)
    {
        _categoryManger = categoryManger;
        _attributeManger = attributeManger;
    }
    public override Task<PagedResultDto<LiteAttributeDto>> GetAllAsync(PagedAttributeResultRequest input)
    {
        return base.GetAllAsync(input);
    }
    public override async Task<AttributeDto> CreateAsync(CreateAttributeDto input)
    {
        List<Category> categories = await _categoryManger.GetAllByListIdsAsync(input.CategoryIds);
        var entity = MapToEntity(input);
        entity.Categories = categories;
        await _attributeManger.InsertAsync(entity);
        return MapToEntityDto(entity);
    }
    public override async Task DeleteAsync(EntityDto<int> input)
    {
        var entity = await _attributeManger.GetEntityByIdAsync(input.Id);
        entity.Translations.Clear();
        entity.Categories.Clear();
        await CurrentUnitOfWork.SaveChangesAsync();
        await base.DeleteAsync(input);
    }
    public override async Task<AttributeDto> UpdateAsync(UpdateAttributeDto input)
    {
        List<Category> categories = await _categoryManger.GetAllByListIdsAsync(input.CategoryIds);
        var entity = await Repository.GetAll().Include(x => x.Translations).FirstOrDefaultAsync(x => x.Id == input.Id);
        entity.Translations.Clear();
        entity.Categories.Clear();
        await CurrentUnitOfWork.SaveChangesAsync();
        MapToEntity(input, entity);
        entity.Categories = categories;
        await CurrentUnitOfWork.SaveChangesAsync();
        return MapToEntityDto(entity);
    }
    public override async Task<AttributeDto> GetAsync(EntityDto<int> input)
    {
        var entity = await _attributeManger.GetEntityByIdAsync(input.Id);
        return MapToEntityDto(entity);
    }
    protected override IQueryable<Attribute> CreateFilteredQuery(PagedAttributeResultRequest input)
    {
        var data = base.CreateFilteredQuery(input);
        if (input.CategoryId.HasValue)
            data = data.Where(x => x.Categories.Select(x => x.Id).Contains(input.CategoryId.Value));
        data = data.Include(x => x.Translations);
        return data;
    }
}
