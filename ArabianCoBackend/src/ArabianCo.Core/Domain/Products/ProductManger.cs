using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.EntityFrameworkCore.Repositories;
using ArabianCo.Domain.AttributeValues;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArabianCo.Domain.Products;

internal class ProductManger : DomainService, IProductManger
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<AttributeValue> _producesAttributeRepository;

    public ProductManger(IRepository<Product> productRepository, IRepository<AttributeValue> producesAttributeRepository)
    {
        _productRepository = productRepository;
        _producesAttributeRepository = producesAttributeRepository;
    }
    public async Task DeleteRangeAttributeValues(ICollection<AttributeValue> attributeValues)
    {
        _producesAttributeRepository.RemoveRange(attributeValues);
    }
    public async Task<Product> GetEntityById(int id)
    {
        Product entity = await _productRepository.GetAll()
            .Include(x => x.Translations.Where(t => !t.IsDeleted))
            .Include(x => x.AttributeValues)
            .ThenInclude(x => x.Attribute)
            .ThenInclude(x => x.Translations.Where(t => !t.IsDeleted))
            .Include(x => x.AttributeValues)
            .ThenInclude(x => x.Translations.Where(t => !t.IsDeleted))
            .Include(x => x.Category)
            .ThenInclude(x => x.Translations.Where(t => !t.IsDeleted))
            .Include(x => x.Brand)
            .ThenInclude(x => x.Translations.Where(t => !t.IsDeleted))
            .FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            throw new EntityNotFoundException(typeof(Product), id);
        return entity;
    }

    public Task<int> InsertAndGetIdAsync(Product product)
    {
        product.IsActive = true;
        return _productRepository.InsertAndGetIdAsync(product);
    }

    public async Task UpdateAsync(Product product)
    {
        await _productRepository.UpdateAsync(product);
    }
}
