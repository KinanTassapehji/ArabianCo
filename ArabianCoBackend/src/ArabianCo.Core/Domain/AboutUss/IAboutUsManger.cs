using Abp.Domain.Services;
using System.Threading.Tasks;

namespace ArabianCo.Domain.AboutUss;

public interface IAboutUsManger:IDomainService
{
    Task<AboutUs> GetEntityByIdAsync(int Id);
    Task SwitchActivation(int Id);
}
