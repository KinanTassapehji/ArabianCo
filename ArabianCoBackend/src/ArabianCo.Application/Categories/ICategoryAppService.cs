using ArabianCo.Categories.Dto;
using ArabianCo.CrudAppServiceBase;

namespace ArabianCo.Categories;

public interface ICategoryAppService: IArabianCoAsyncCrudAppService<CategoryDetaisDto,int,LiteCategoryDto,PagedCategoryResultRequestDto,CreateCategoryDto,UpdateCategoryDto>
{
}
