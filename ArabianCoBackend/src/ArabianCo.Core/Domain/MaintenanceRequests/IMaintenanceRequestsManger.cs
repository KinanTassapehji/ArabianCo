using Abp.Domain.Services;
using System.Threading.Tasks;

namespace ArabianCo.Domain.MaintenanceRequests;

public interface IMaintenanceRequestsManger : IDomainService
{
    Task InsertAsync(MaintenanceRequest maintenanceRequest);
    Task<MaintenanceRequest> GetEntityByIdAsync(int id);
}
