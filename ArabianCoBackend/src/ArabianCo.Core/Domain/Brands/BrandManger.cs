using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArabianCo.Domain.Brands;

internal class BrandManger : DomainService, IBrandManger
{
    private readonly IRepository<Brand> _brandRepository;
    private readonly IRepository<BrandTranslation> _brandTranslationRepository;

    public BrandManger(IRepository<Brand> brandRepository, IRepository<BrandTranslation> brandTranslationRepository)
    {
        _brandRepository = brandRepository;
        _brandTranslationRepository = brandTranslationRepository;
    }

    public async Task<bool> CheckIfBrandIsExist(List<BrandTranslation> translations)
    {
        var brands = await _brandTranslationRepository.GetAll().ToListAsync();
        foreach (var existingBrand in brands)
        {
            foreach (var brand in translations)
                if (existingBrand.Name == brand.Name && existingBrand.Language == brand.Language)
                    return true;
        }
        return false;
    }

    public async Task DeleteAsync(int id)
    {
        await _brandRepository.DeleteAsync(id);
    }

    public async Task<Brand> GetEntityByIdAsync(int id)
    {
        var entity = await _brandRepository.GetAll().Where(x => x.Id == id).Include(x => x.Translations.Where(t => !t.IsDeleted)).FirstOrDefaultAsync();
        if (entity == null)
            throw new EntityNotFoundException(typeof(Brand), id);
        return entity;
    }

    public async Task<Brand> GetLiteEntityByIdAsync(int id)
    {
        return await _brandRepository.GetAsync(id);
    }

    public Task<int> InsertAndGetIdAsync(Brand entity)
    {
        return _brandRepository.InsertAndGetIdAsync(entity);
    }

    public async Task InsertAsync(Brand entity)
    {
        await _brandRepository.InsertAsync(entity);
    }

    public async Task UpdateAsync(Brand entity)
    {
        await _brandRepository.UpdateAsync(entity);
    }
}
