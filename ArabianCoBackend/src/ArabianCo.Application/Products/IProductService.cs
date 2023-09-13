using ArabianCo.CrudAppServiceBase;
using ArabianCo.Products.Dto;

namespace ArabianCo.Products;

public interface IProductService:IArabianCoAsyncCrudAppService<ProductDto,int,LiteProductDto,PagedProductResultRequestDto,CreateProductDto,UpdateProductDto>
{
}
