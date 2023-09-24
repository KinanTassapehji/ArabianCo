using ArabianCo.CrudAppServiceBase;
using ArabianCo.Questions.Dto;

namespace ArabianCo.Questions;

public interface IQuestionAppService:IArabianCoAsyncCrudAppService<QuestionDto,int,QuestionDto,PagedQuestionResultRequest,CreateQuestionDto,UpdateQuestionDto>
{
}
