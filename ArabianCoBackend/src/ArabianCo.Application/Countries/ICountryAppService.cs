using ArabianCo.Countries.Dto;
using ArabianCo.CrudAppServiceBase;

namespace ArabianCo.Countries;

public interface ICountryAppService:IArabianCoAsyncCrudAppService<CountryDetailsDto, int, CountryDto, PagedCountryResultRequestDto,
        CreateCountryDto, UpdateCountryDto>
{
}
