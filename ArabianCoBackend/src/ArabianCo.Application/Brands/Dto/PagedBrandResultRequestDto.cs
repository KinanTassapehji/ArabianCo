using Abp.Application.Services.Dto;
using System.Text.Json.Serialization;

namespace ArabianCo.Brands.Dto;

public class PagedBrandResultRequestDto : PagedResultRequestDto
{
    [JsonIgnore]
    public long? UserId { get; set; }
}

