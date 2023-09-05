using Abp.Application.Services.Dto;

namespace ArabianCo.FrequentlyQuestionService.Dto;

public class UpdateFrequentlyQuestionDto : CreateFrequentlyQuestionDto, IEntityDto
{
    public int Id { get; set; }
}
