using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace ArabianCo.Domain.Attributes;

public class AttributeTranslation : FullAuditedEntity, IEntityTranslation<Attribute>
{
    public Attribute Core { get; set; }
    public int CoreId { get; set; }
    public string Language { get; set; }
    public string Name { get; set; }
}
