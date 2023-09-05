using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ArabianCo.Domain.Cities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArabianCo.Domain.Countries;

public class Country : FullAuditedEntity, IMultiLingualEntity<CountryTranslation>
{
    public Country()
    {

        Cities = new HashSet<City>();
        Translations = new HashSet<CountryTranslation>();
    }

    public bool IsActive { get; set; }
    [Required]
    [StringLength(5)]
    public string DialCode { get; set; }
    public virtual ICollection<City> Cities { get; set; }
    public ICollection<CountryTranslation> Translations { get; set; }
}
