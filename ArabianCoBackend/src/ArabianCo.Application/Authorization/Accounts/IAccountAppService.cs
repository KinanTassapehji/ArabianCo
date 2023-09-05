using System.Threading.Tasks;
using Abp.Application.Services;
using ArabianCo.Authorization.Accounts.Dto;

namespace ArabianCo.Authorization.Accounts
{
    internal interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
