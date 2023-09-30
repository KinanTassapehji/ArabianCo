using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace ArabianCo.Products.Dto;

public class PagedProductResultRequestDto : PagedResultRequestDto
{
    public string Keyword { get; set; }
    public List<int> BrandIds { get; set; }
    public List<int> CategoryIds { get; set; }
    public bool? IsActive { get; set; }
}
