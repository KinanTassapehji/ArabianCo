using Abp.AutoMapper;
using ArabianCo.Domain.Countries;
using System.ComponentModel.DataAnnotations;


namespace ArabianCo.Countries.Dto
{
    /// <summary>
    /// Post Category Translation Dto
    /// </summary>
    [AutoMap(typeof(CountryTranslation))]
    public class CountryTranslationDto
    {
        /// <summary>
        /// Name
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Language
        /// </summary>
        [Required]
        public string Language { get; set; }
    }
}
