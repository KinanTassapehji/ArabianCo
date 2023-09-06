using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using ArabianCo.Attributes.Dto;
using ArabianCo.CrudAppServiceBase;
using ArabianCo.Domain.Attributes;
using ArabianCo.Domain.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ArabianCo.Attributes;

public class AttributeAppService : ArabianCoAsyncCrudAppService<Attribute, AttributeDto, int, LiteAttributeDto, PagedAttributeResultRequest, CreateAttributeDto, UpdateAttributeDto>, IAttributeAppService
{
    private readonly ICategoryManger _categoryManger;
    public AttributeAppService(IRepository<Attribute, int> repository, ICategoryManger categoryManger) : base(repository)
    {
        _categoryManger = categoryManger;
    }
    public override Task<PagedResultDto<LiteAttributeDto>> GetAllAsync(PagedAttributeResultRequest input)
    {
        return base.GetAllAsync(input);
    }
    public override async Task<AttributeDto> CreateAsync(CreateAttributeDto input)
    {
        if (input.CategoryId.HasValue)
            await _categoryManger.GetLiteEntityByIdAsync(input.CategoryId.Value);
        var entity = await base.CreateAsync(input);
        return entity;
    }
    public override async Task<AttributeDto> UpdateAsync(UpdateAttributeDto input)
    {
        if(input.CategoryId.HasValue)
            await _categoryManger.GetLiteEntityByIdAsync(input.CategoryId.Value);
        var entity = await Repository.GetAll().Include(x=>x.Translations).FirstOrDefaultAsync(x=>x.Id==input.Id);
        entity.Translations.Clear();
        MapToEntity(input, entity);
        await CurrentUnitOfWork.SaveChangesAsync();
        return MapToEntityDto(entity);
    }
    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<AttributeDto> GetAsync(EntityDto<int> input)
    {
        return base.GetAsync(input);
    }
    protected override IQueryable<Attribute> CreateFilteredQuery(PagedAttributeResultRequest input)
    {
        var data = base.CreateFilteredQuery(input);
        if(input.CategoryId.HasValue)
            data = data.Where(x=>x.CategoryId == input.CategoryId.Value);
        data = data.Include(x => x.Category.Translations);
        data = data.Include(x => x.Translations);
        return data;
    }
}
