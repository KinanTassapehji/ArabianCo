using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
namespace ArabianCo.Domain.Countries
{
    public class CountryTranslation : FullAuditedEntity, IEntityTranslation<Country>
    {
        public string Name { get; set; }
        public Country Core { get; set; }
        public int CoreId { get; set; }
        public string Language { get; set; }
    }
}
