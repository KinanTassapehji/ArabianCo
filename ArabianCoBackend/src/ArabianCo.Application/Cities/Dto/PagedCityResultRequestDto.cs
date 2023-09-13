using Abp.Application.Services.Dto;
using System.Text.Json.Serialization;

namespace ArabianCo.Cities.Dto
{
    public class PagedCityResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public int? CountryId { get; set; }
        public bool? isActive { get; set; }
    }
}
