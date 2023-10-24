using Abp.Application.Services.Dto;

namespace ArabianCo.Categories.Dto;

public class PagedCategoryResultRequestDto : PagedResultRequestDto
{
    public bool? IsParent { get; set; }
    public int? ParentCategoryId { get; set; }
}
