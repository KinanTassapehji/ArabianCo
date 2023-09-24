using ArabianCo.Domain.Questions;
using ArabianCo.Questions.Dto;
using AutoMapper;

namespace ArabianCo.Questions.Mapper;

internal class QuestionMapProfile:Profile
{
    public QuestionMapProfile()
    {
        CreateMap<CreateQuestionDto, Question>();
        CreateMap<UpdateQuestionDto, Question>();
        CreateMap<Question,QuestionDto>();
    }
}
