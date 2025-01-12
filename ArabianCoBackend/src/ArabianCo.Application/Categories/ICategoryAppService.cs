using ArabianCo.Categories.Dto;
using ArabianCo.CrudAppServiceBase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArabianCo.Categories;

public interface ICategoryAppService : IArabianCoAsyncCrudAppService<CategoryDetaisDto, int, LiteCategoryDto, PagedCategoryResultRequestDto, CreateCategoryDto, UpdateCategoryDto>
{
    Task<List<IndexDto>> GetCategoriesForProductAndAttribute();
}
