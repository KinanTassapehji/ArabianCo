using ArabianCo.Attributes.Dto;
using ArabianCo.CrudAppServiceBase;

namespace ArabianCo.Attributes;

public interface IAttributeAppService:IArabianCoAsyncCrudAppService<AttributeDto,int,LiteAttributeDto,PagedAttributeResultRequest,CreateAttributeDto,UpdateAttributeDto>
{
}
