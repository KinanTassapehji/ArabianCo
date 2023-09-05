using Abp.Application.Services.Dto;

namespace ArabianCo.Attributes.Dto;

public class PagedAttributeResultRequest : PagedResultRequestDto
{
    public int? CategoryId { get; set; }
}
