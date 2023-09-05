using Abp.AutoMapper;
using ArabianCo.Domain.Brands;

namespace ArabianCo.Brands.Dto;

[AutoMap(typeof(BrandTranslation))]
public class BrandTranslationDto
{
    public string Name { get; set; }
    public string Language { get; set; }
    public string Description { get; set; }
}
