using ArabianCo.Areas.Dto;
using ArabianCo.CrudAppServiceBase;

namespace ArabianCo.Areas
{
    public interface IAreaAppService : IArabianCoAsyncCrudAppService<AreaDetailsDto, int, LiteAreaDto
        , PagedAreaResultRequestDto,
        CreateAreaDto, UpdateAreaDto>
    {

    }
}
