using ArabianCo.Cities.Dto;
using ArabianCo.CrudAppServiceBase;

namespace ArabianCo.Cities
{
    public interface ICityAppService : IArabianCoAsyncCrudAppService<CityDetailsDto, int, LiteCityDto, PagedCityResultRequestDto,
        CreateCityDto, UpdateCityDto>
    {

    }
}
