using Abp.AutoMapper;
using ArabianCo.Domain.Categories;

namespace ArabianCo.Categories.Dto;

[AutoMap(typeof(CategoryTranslation))]
public class CategoryTranslationDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Language { get; set; }
}
