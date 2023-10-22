using ArabianCo.AboutUss.Dto;
using ArabianCo.CrudAppServiceBase;

namespace ArabianCo.AboutUss;

public interface IAboutUsAppService:IArabianCoAsyncCrudAppService<AboutUsDto,int,AboutUsDto,PagedAboutUsResultRequestDto,CreateAboutUsDto,UpdateAboutUsDto>
{
}
