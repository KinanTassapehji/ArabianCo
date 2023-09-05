using Abp.Domain.Services;
using System.Threading.Tasks;

namespace ArabianCo.Domain.Products;

public interface IProductManger:IDomainService
{
    Task<Product> GetEntityById(int id);
}
