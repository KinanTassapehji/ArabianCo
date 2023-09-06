using Abp.Application.Services.Dto;

namespace ArabianCo.Products.Dto;

public class PagedProductResultRequestDto : PagedResultRequestDto
{
    public int? BrandId { get; set; }
    public int? CategoryId { get; set; }
    public bool? IsActive { get; set; }
}
