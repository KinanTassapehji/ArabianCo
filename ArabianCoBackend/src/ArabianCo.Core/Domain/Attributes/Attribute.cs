using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ArabianCo.Domain.Categories;
using System.Collections.Generic;

namespace ArabianCo.Domain.Attributes;

public class Attribute : FullAuditedEntity, IMultiLingualEntity<AttributeTranslation>
{
    public Attribute()
    {
        Translations = new HashSet<AttributeTranslation>();
        Categories = new HashSet<Category>();
    }
    public ICollection<Category> Categories { get; set; }
    public ICollection<AttributeTranslation> Translations { get; set; }
}
