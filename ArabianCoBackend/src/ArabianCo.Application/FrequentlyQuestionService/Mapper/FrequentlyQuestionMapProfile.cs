using ArabianCo.Domain.FrequentlyQuestions;
using ArabianCo.FrequentlyQuestionService.Dto;
using AutoMapper;

namespace KeyFinder.FrequentlyQuestionService.Mapper
{
    public class FrequentlyQuestionMapProfile : Profile
    {
        public FrequentlyQuestionMapProfile()
        {
            CreateMap<CreateFrequentlyQuestionDto, FrequentlyQuestion>();
            //CreateMap<FrequentlyQuestion, FrequentlyQuestionDetailsDto>();
        }
    }
}
