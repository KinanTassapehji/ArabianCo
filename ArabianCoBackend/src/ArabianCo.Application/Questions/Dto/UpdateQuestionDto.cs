using Abp.Application.Services.Dto;

namespace ArabianCo.Questions.Dto;

public class UpdateQuestionDto:CreateQuestionDto, IEntityDto
{
    public int Id { get; set; }
}
