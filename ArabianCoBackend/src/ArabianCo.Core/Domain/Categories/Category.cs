using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ArabianCo.Domain.Attributes;
using System.Collections.Generic;

namespace ArabianCo.Domain.Categories;

public class Category:FullAuditedEntity,IMultiLingualEntity<CategoryTranslation>
{
    public Category()
    {
        Translations = new HashSet<CategoryTranslation>();
        Attributes = new HashSet<Attribute>();
    }
    public ICollection<CategoryTranslation> Translations { get; set; }
    public ICollection<Attribute> Attributes { get; set; }
}
