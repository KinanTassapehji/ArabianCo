using Abp.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArabianCo.Domain.Categories
{
    public interface ICategoryManger:IDomainService
    {
        Task<Category> GetEntityByIdAsync(int id);
        Task<bool> CheckIfCategoryIsExist(List<CategoryTranslation> translations);
        Task<Category> GetLiteEntityByIdAsync(int id);
        Task InsertAsync(Category entity);
        Task<int> InsertAndGetIdAsync(Category entity);
        Task UpdateAsync(Category entity);
        Task<List<Category>> GetAllByListIdsAsync(List<int> ids);
        Task DeleteAsync(int id);
    }
}
