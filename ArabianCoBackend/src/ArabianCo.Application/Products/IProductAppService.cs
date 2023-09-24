using ArabianCo.CrudAppServiceBase;
using ArabianCo.Products.Dto;

namespace ArabianCo.Products;

public interface IProductAppService:IArabianCoAsyncCrudAppService<ProductDto,int,LiteProductDto,PagedProductResultRequestDto,CreateProductDto,UpdateProductDto>
{
}
