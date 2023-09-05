using Abp.AutoMapper;
using ArabianCo.Domain.FrequentlyQuestions;

namespace ArabianCo.FrequentlyQuestionService.Dto;

[AutoMap(typeof(FrequentlyQuestionTranslation))]

public class FrequentlyQuestionTranslationDto
{
    public string Question { get; set; }
    public string Answer { get; set; }
    public string Language { get; set; }

}
