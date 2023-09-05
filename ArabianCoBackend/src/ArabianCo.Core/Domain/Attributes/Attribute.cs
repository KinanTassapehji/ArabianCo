using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ArabianCo.Domain.Categories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArabianCo.Domain.Attributes;

public class Attribute : FullAuditedEntity, IMultiLingualEntity<AttributeTranslation>
{
    public Attribute()
    {
        Translations = new HashSet<AttributeTranslation>();
    }
    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; }
    public ICollection<AttributeTranslation> Translations { get; set; }
}
