using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore.Update;

namespace ArabianCo.Attributes.Dto;

public class UpdateAttributeDto : CreateAttributeDto, IEntityDto
{
    public int Id { get; set; }
}
