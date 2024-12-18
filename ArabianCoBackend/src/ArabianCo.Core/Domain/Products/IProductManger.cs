using Abp.Domain.Services;
using ArabianCo.Domain.AttributeValues;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArabianCo.Domain.Products;

public interface IProductManger : IDomainService
{
    Task<Product> GetEntityById(int id);
    Task<int> InsertAndGetIdAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteRangeAttributeValues(ICollection<AttributeValue> attributeValues);
}
