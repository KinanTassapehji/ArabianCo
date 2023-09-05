using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace ArabianCo.FrequentlyQuestionService.Dto;

public class LiteFrequentlyQuestionDto : EntityDto<int>
{
    public string Question { get; set; }
    public string Answer { get; set; }
    public List<FrequentlyQuestionTranslationDto> Translations { get; set; }
}
