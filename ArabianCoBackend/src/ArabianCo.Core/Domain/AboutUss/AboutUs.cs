using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace ArabianCo.Domain.AboutUss;

public class AboutUs : FullAuditedEntity, IMultiLingualEntity<AboutUsTranslation>
{
    public AboutUs()
    {
        Translations = new HashSet<AboutUsTranslation>();
    }
    public bool IsActive { get; set; }
    public int? ShowOrder { get; set; }
    public ICollection<AboutUsTranslation> Translations { get; set; }
}
