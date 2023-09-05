using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace ArabianCo.Domain.Categories;

public class CategoryTranslation : FullAuditedEntity, IEntityTranslation<Category>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Category Core { get; set; }
    public int CoreId { get; set; }
    public string Language { get; set; }
}
