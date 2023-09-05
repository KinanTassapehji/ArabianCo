using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace ArabianCo.Domain.Brands;

public class Brand : FullAuditedEntity, IMultiLingualEntity<BrandTranslation>
{
    public Brand()
    {
        Translations = new HashSet<BrandTranslation>();
    }
    public ICollection<BrandTranslation> Translations { get; set; }
    public bool IsActive { get; set; }
}
