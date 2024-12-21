using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace ArabianCo.Domain.Products;

public class ProductTranslation : FullAuditedEntity, IEntityTranslation<Product>
{
    public Product Core { get; set; }
    public int CoreId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Language { get; set; }
}
