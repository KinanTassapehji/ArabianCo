using Abp.Application.Services.Dto;

namespace ArabianCo.Areas.Dto
{
    public class PagedAreaResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public int? CityId { get; set; }
        public bool? IsActive { get; set; }
    }
}
