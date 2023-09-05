using ArabianCo.Brands.Dto;
using ArabianCo.CrudAppServiceBase;

namespace ArabianCo.Brands;

public interface IBrandAppService:IArabianCoAsyncCrudAppService<BrandDto,int,LiteBrandDto,PagedBrandResultRequestDto,CreateBrandDto,UpdateBrandDto>
{
}
