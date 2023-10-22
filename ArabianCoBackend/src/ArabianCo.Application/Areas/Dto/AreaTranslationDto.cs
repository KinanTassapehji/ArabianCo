using Abp.AutoMapper;
using ArabianCo.Domain.Areas;
using System.ComponentModel.DataAnnotations;

namespace ArabianCo.Areas.Dto;

[AutoMap(typeof(AreaTranslation))]
public class AreaTranslationDto
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
