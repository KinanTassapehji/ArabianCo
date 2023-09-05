using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace ArabianCo.Domain.Products;

internal class ProductMMangrt : DomainService, IProductManger
{
    private readonly IRepository<Product> _productRepository;

    public ProductMMangrt(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> GetEntityById(int id)
    {
        Product entity = await _productRepository.GetAll().Include(x=>x.AttributeValues)
            .ThenInclude(x=>x.Attribute.Translations)
            .Include(x=>x.Category.Translations)
            .Include(x=>x.Brand.Translations)
            .FirstOrDefaultAsync(x=>x.Id==id);
        if (entity == null)
            throw new EntityNotFoundException(typeof(Product), id);
        return entity;
    }
}
