using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ArabianCo.Domain.AttributeValues;
using ArabianCo.Domain.Brands;
using ArabianCo.Domain.Categories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArabianCo.Domain.Products;

public class Product : FullAuditedEntity, IMultiLingualEntity<ProductTranslation>
{
    public Product()
    {
        AttributeValues = new HashSet<AttributeValue>();
        Translations = new HashSet<ProductTranslation>();
    }
    public string ModelNumber { get; set; }
    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; }
    public int BrandId { get; set; }
    [ForeignKey(nameof(BrandId))]
    public virtual Brand Brand { get; set; }
    public bool IsActive { get; set; }
    public ICollection<AttributeValue> AttributeValues { get; set; }
    public ICollection<ProductTranslation> Translations { get; set; }
}
