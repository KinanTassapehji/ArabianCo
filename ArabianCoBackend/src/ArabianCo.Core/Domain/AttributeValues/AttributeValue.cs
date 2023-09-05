using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ArabianCo.Domain.Attributes;
using ArabianCo.Domain.Products;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArabianCo.Domain.AttributeValues;

public class AttributeValue:FullAuditedEntity, IMultiLingualEntity<AttributeValueTranslation>
{
    public AttributeValue()
    {
        Translations = new HashSet<AttributeValueTranslation>();
    }
    public int AttributeId { get; set; }
    [ForeignKey(nameof(AttributeId))]
    public virtual Attribute Attribute { get; set; }
    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; }
    public ICollection<AttributeValueTranslation> Translations { get; set; }
}
