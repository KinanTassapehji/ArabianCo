using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace ArabianCo.Categories.Dto;

public class CategoryDto:EntityDto<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsParent { get; set; }
    public List<LiteCategoryDto> SubCategories { get; set; }
    public List<CategoryTranslationDto> Translations { get; set; }
}
