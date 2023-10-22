using Abp.Application.Services.Dto;

namespace ArabianCo.AboutUss.Dto;

public class UpdateAboutUsDto:CreateAboutUsDto, IEntityDto
{
    public int Id { get; set; }
}
