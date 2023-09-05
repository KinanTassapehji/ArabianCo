using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace ArabianCo.Domain.AttributeValues;

public class AttributeValueTranslation : FullAuditedEntity, IEntityTranslation<AttributeValue>
{
    public AttributeValue Core { get; set; }
    public int CoreId { get; set; }
    public string Language { get; set; }
    public string Value { get; set; }
}
