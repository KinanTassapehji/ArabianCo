using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Entities;

namespace ArabianCo.Domain.MaintenanceRequests;

internal class MaintenanceRequestManger : DomainService, IMaintenanceRequestsManger
{
    private readonly IRepository<MaintenanceRequest> _repository;

    public MaintenanceRequestManger(IRepository<MaintenanceRequest> repository)
    {
        _repository = repository;
    }

    public async Task<MaintenanceRequest> GetEntityByIdAsync(int id)
    {
        var entity = await _repository.GetAll().Where(x=>x.Id==id)
            .Include(x=>x.Brand)
            .Include(x=>x.Category)
            .Include(x=>x.Area).ThenInclude(x=>x.Translations)
            .Include(x=>x.Area.City).ThenInclude(x=>x.Translations)
            .Include(x=>x.Area.City.Country).ThenInclude(x=>x.Translations).FirstOrDefaultAsync();
        if (entity == null)
            throw new EntityNotFoundException(typeof(MaintenanceRequest), id);
        return entity;
    }

    public async Task InsertAsync(MaintenanceRequest maintenanceRequest)
    {
        await _repository.InsertAsync(maintenanceRequest);
    }
}
