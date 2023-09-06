using Abp.Application.Services.Dto;

namespace ArabianCo.Products.Dto;

public class UpdateProductDto:CreateProductDto, IEntityDto
{
    public int Id { get; set; }
}
