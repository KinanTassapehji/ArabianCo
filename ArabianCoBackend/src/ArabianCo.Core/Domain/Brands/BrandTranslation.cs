using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace ArabianCo.Domain.Brands;

public class BrandTranslation : FullAuditedEntity, IEntityTranslation<Brand>
{
    public Brand Core { get; set; }
    public int CoreId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Language { get; set; }
}
