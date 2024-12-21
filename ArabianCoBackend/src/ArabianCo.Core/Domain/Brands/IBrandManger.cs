using Abp.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArabianCo.Domain.Brands;

public interface IBrandManger : IDomainService
{
    Task<Brand> GetEntityByIdAsync(int id);
    Task<bool> CheckIfBrandIsExist(List<BrandTranslation> translations);
    Task<Brand> GetLiteEntityByIdAsync(int id);
    Task InsertAsync(Brand entity);
    Task<int> InsertAndGetIdAsync(Brand entity);
    Task UpdateAsync(Brand entity);
    Task DeleteAsync(int id);
}
