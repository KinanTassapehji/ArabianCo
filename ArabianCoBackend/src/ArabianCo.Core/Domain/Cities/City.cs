using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ArabianCo.Domain.Areas;
using ArabianCo.Domain.Countries;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArabianCo.Domain.Cities
{
    //city model
    public class City : FullAuditedEntity, IMultiLingualEntity<CityTranslation>
    {
        public City()
        {
            Areas = new HashSet<Area>();
            Translations = new HashSet<CityTranslation>();
        }

        public int CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
        public ICollection<CityTranslation> Translations { get; set; }
    }
}
