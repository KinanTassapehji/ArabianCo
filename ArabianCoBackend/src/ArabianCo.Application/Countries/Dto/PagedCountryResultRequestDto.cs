using Abp.Application.Services.Dto;

namespace ArabianCo.Countries.Dto
{
    public class PagedCountryResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
