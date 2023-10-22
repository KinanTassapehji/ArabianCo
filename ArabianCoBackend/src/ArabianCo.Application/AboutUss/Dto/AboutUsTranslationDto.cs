using Abp.AutoMapper;
using ArabianCo.Domain.AboutUss;
using System.ComponentModel.DataAnnotations;

namespace ArabianCo.AboutUss.Dto;

[AutoMap(typeof(AboutUsTranslation))]
public class AboutUsTranslationDto
{
    [Required]
    public string Language { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
}
