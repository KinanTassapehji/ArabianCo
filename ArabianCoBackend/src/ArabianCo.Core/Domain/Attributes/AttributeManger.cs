using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using ArabianCo.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArabianCo.Domain.Attributes;

internal class AttributeManger : DomainService, IAttributeManger
{
    private readonly IRepository<Attribute> _attributeRepository;
    private readonly IRepository<AttributeTranslation> _attributeTranslationRepository;
    private readonly IRepository<Category> _categoryRepository;

    public AttributeManger(IRepository<Attribute> attributeRepository, IRepository<Category> categoryRepository, IRepository<AttributeTranslation> attributeTranslationRepository)
    {
        _attributeRepository = attributeRepository;
        _categoryRepository = categoryRepository;
        _attributeTranslationRepository = attributeTranslationRepository;
    }

    public async Task<bool> CheckIfAttributeIsExist(List<AttributeTranslation> translations)
    {

        var attributes = await _attributeTranslationRepository.GetAll().ToListAsync();
        foreach (var existingAttribute in attributes)
        {
            foreach (var translation in translations)
                if (existingAttribute.Name == translation.Name && existingAttribute.Language == translation.Language)
                    return true;
        }
        return false;
    }

    public async Task<Attribute> GetEntityByIdAsync(int id)
    {
        var entity = await _attributeRepository.GetAll().Where(x => x.Id == id)
            .Include(x => x.Translations.Where(t => !t.IsDeleted))
            .Include(x => x.Categories).ThenInclude(x => x.Translations.Where(t => !t.IsDeleted))
            .FirstOrDefaultAsync();
        if (entity == null)
            throw new EntityNotFoundException(typeof(Attribute), id);
        return entity;
    }

    public async Task InsertAsync(Attribute attribute)
    {
        await _attributeRepository.InsertAsync(attribute);
    }
}
