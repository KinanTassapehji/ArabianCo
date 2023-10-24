using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ArabianCo.Domain.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArabianCo.Domain.Categories;

public class Category:FullAuditedEntity,IMultiLingualEntity<CategoryTranslation>
{
    public Category()
    {
        Translations = new HashSet<CategoryTranslation>();
        Attributes = new HashSet<Attribute>();
    }
    [DefaultValue(false)]
    public bool IsParent { get; set; }
    public int? ParentCategoryId { get; set; }
    [ForeignKey(nameof(ParentCategoryId))]
    public Category ParentCategory { get; set; }
    public ICollection<CategoryTranslation> Translations { get; set; }
    public ICollection<Attribute> Attributes { get; set; }
}
