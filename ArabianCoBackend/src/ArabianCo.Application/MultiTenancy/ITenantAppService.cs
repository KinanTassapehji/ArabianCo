using Abp.Application.Services;
using ArabianCo.MultiTenancy.Dto;

namespace ArabianCo.MultiTenancy
{
    internal interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

