using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using ArabianCo.Domain.FrequentlyQuestions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ArabianCo.FrequentlyQuestions;

public class FrequentlyQuestionManager : DomainService, IFrequentlyQuestionManager
{
    private readonly IRepository<FrequentlyQuestion> _frequentlyQuestionRepository;
    public FrequentlyQuestionManager(IRepository<FrequentlyQuestion> frequentlyQuestionRepository)
    {
        _frequentlyQuestionRepository = frequentlyQuestionRepository;
    }

    public async Task<FrequentlyQuestion> GetEntityByIdAsync(int id)
    {
        var entity = await _frequentlyQuestionRepository.GetAll().Where(x => x.Id == id).Include(x => x.Translations).FirstOrDefaultAsync();
        if (entity == null)
            throw new EntityNotFoundException(typeof(FrequentlyQuestion), id);
        return entity;
    }
}
