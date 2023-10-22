using Abp.Application.Services.Dto;

namespace ArabianCo.AboutUss.Dto;

public class PagedAboutUsResultRequestDto:PagedResultRequestDto
{
    public bool? IsActive { get; set; }
}
