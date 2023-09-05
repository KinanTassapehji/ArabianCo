using Abp.Domain.Services;
using ArabianCo.Domain.FrequentlyQuestions;
using System.Threading.Tasks;

namespace ArabianCo.FrequentlyQuestions;

public interface IFrequentlyQuestionManager : IDomainService
{
    Task<FrequentlyQuestion> GetEntityByIdAsync(int id);
}
