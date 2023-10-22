using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Entities;

namespace ArabianCo.Domain.AboutUss;

internal class AboutUsManger : DomainService, IAboutUsManger
{
    private readonly IRepository<AboutUs> _repository;
    public AboutUsManger(IRepository<AboutUs> repository)
    {
        _repository = repository;
    }
    public Task<AboutUs> GetEntityByIdAsync(int Id)
    {
        var entity = _repository.GetAll().Where(x => x.Id == Id).Include(x => x.Translations).FirstOrDefaultAsync();
        if (entity == null)
            throw new EntityNotFoundException(typeof(AboutUs), Id);
        return entity;
    }

    public async Task SwitchActivation(int Id)
    {
        var entity = await _repository.GetAsync(Id);
        entity.IsActive = !entity.IsActive;
        await _repository.UpdateAsync(entity);
    }
}
