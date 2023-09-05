using ArabianCo.CrudAppServiceBase;
using ArabianCo.FrequentlyQuestionService.Dto;

namespace KeyFinder.FrequentlyQuestionService
{
    public interface IFrequentlyQuestionAppService : IArabianCoAsyncCrudAppService<FrequentlyQuestionDetailsDto, int, LiteFrequentlyQuestionDto, PagedFrequentlyQuestionResultRequestDto,
         CreateFrequentlyQuestionDto, UpdateFrequentlyQuestionDto>
    {
    }
}
