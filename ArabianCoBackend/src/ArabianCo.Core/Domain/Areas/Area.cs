using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ArabianCo.Domain.Cities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArabianCo.Domain.Areas
{
    public class Area : FullAuditedEntity, IMultiLingualEntity<AreaTranslation>
    {
        public Area()
        {
            Translations = new HashSet<AreaTranslation>();
        }
        public bool IsActive { get; set; }
        public int CityId { get; set; }
        [ForeignKey(nameof(CityId))]
        public virtual City City { get; set; }
        public ICollection<AreaTranslation> Translations { get; set; }

    }
}
