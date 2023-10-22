using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace ArabianCo.Domain.AboutUss;

public class AboutUsTranslation : FullAuditedEntity, IEntityTranslation<AboutUs>
{
    public AboutUs Core { get; set; }
    public int CoreId { get ; set ; }
    public string Language { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
