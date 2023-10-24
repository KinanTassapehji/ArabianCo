using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Linq;
using Abp.Domain.Entities;

namespace ArabianCo.Domain.Categories;

internal class CategoryManger : DomainService, ICategoryManger
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<CategoryTranslation> _categoryTranslationRepository;
    public CategoryManger(IRepository<Category> categoryRepository, IRepository<CategoryTranslation> categoryTranslationRepository)
    {
        _categoryRepository = categoryRepository;
        _categoryTranslationRepository = categoryTranslationRepository;
    }
    public async Task<bool> CheckIfCategoryIsExist(List<CategoryTranslation> Translations)
    {
        var categories = await _categoryTranslationRepository.GetAll().ToListAsync();
        foreach (var Translation in Translations)
        {
            foreach (var category in categories)
                if (category.Name == Translation.Name && category.Language == Translation.Language)
                    return true;
        }
        return false;
    }

    public async Task DeleteAsync(int id)
    {
        await _categoryRepository.DeleteAsync(id);
    }

    public async Task<List<Category>> GetAllByListIdsAsync(List<int> ids)
    {
        var result = await _categoryRepository.GetAll().Where(x=>ids.Contains(x.Id)).ToListAsync();
        var foundIds = result.Select(x => x.Id).ToList();
        var missingIds = ids.Except(foundIds).ToList();
        if(missingIds.Any())
        {
            throw new EntityNotFoundException(typeof(Category),missingIds.First());
        }
        return result;
    }

    public async Task<Category> GetEntityByIdAsync(int id)
    {
        var entity = await _categoryRepository.GetAll().Where(x => x.Id == id).Include(x => x.Translations).FirstOrDefaultAsync();
        if (entity == null)
            throw new EntityNotFoundException(typeof(Category), id);
        return entity;
    }

    public async Task<Category> GetLiteEntityByIdAsync(int id)
    {
        var entity = await _categoryRepository.GetAsync(id);
        if (entity == null)
            throw new EntityNotFoundException(typeof(Category), id);
        return entity;
    }

    public Task<List<Category>> GetSubCategoriesByParentCategoryId(int parentCategoryId)
    {
        return _categoryRepository.GetAll().Where(x=>x.ParentCategoryId == parentCategoryId).Include(x=>x.Translations).ToListAsync();
    }

    public Task<int> InsertAndGetIdAsync(Category entity)
    {
        return _categoryRepository.InsertAndGetIdAsync(entity);
    }

    public async Task InsertAsync(Category entity)
    {
         await _categoryRepository.InsertAsync(entity);
    }

    public async Task UpdateAsync(Category entity)
    {
        await _categoryRepository.UpdateAsync(entity);
    }
}
