using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace ArabianCo.Domain.Areas
{
    public class AreaTranslation : FullAuditedEntity, IEntityTranslation<Area>
    {
        public string Name { get; set; }
        public Area Core { get; set; }
        public int CoreId { get; set; }
        public string Language { get; set; }
    }
}
