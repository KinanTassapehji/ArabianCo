using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ArabianCo.Domain.Products;

internal class ProductManger : DomainService, IProductManger
{
    private readonly IRepository<Product> _productRepository;

    public ProductManger(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> GetEntityById(int id)
    {
        Product entity = await _productRepository.GetAll().IgnoreQueryFilters()
            .Include(x => x.Translations)
            .Include(x => x.AttributeValues)
            .ThenInclude(x => x.Attribute.Translations)
            .Include(x => x.AttributeValues).ThenInclude(x => x.Translations)
            .Include(x => x.Category.Translations)
            .Include(x => x.Brand.Translations)
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
