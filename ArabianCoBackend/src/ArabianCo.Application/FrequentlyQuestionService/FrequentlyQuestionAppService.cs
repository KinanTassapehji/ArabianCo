using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using ArabianCo.CrudAppServiceBase;
using ArabianCo.Domain.FrequentlyQuestions;
using ArabianCo.FrequentlyQuestions;
using ArabianCo.FrequentlyQuestionService.Dto;
using ArabianCo.Localization.SourceFiles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyFinder.FrequentlyQuestionService
{
    public class FrequentlyQuestionAppService : ArabianCoAsyncCrudAppService<FrequentlyQuestion, FrequentlyQuestionDetailsDto, int, LiteFrequentlyQuestionDto, PagedFrequentlyQuestionResultRequestDto,
         CreateFrequentlyQuestionDto, UpdateFrequentlyQuestionDto>, IFrequentlyQuestionAppService
    {
        private readonly IFrequentlyQuestionManager _frequentlyQuestionManager;
        public FrequentlyQuestionAppService(IRepository<FrequentlyQuestion, int> repository, IFrequentlyQuestionManager frequentlyQuestionManager) : base(repository)
        {
            _frequentlyQuestionManager = frequentlyQuestionManager;
        }
        public override async Task<FrequentlyQuestionDetailsDto> GetAsync(EntityDto<int> input)
        {
            var frequentlyQuestion = await _frequentlyQuestionManager.GetEntityByIdAsync(input.Id);
            if (frequentlyQuestion is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound));
            return MapToEntityDto(frequentlyQuestion);
        }
        public override async Task<PagedResultDto<LiteFrequentlyQuestionDto>> GetAllAsync(PagedFrequentlyQuestionResultRequestDto input)
        {
            var result = await base.GetAllAsync(input);
            return result;
        }
        public override async Task<FrequentlyQuestionDetailsDto> CreateAsync(CreateFrequentlyQuestionDto input)
        {
            var Translation = ObjectMapper.Map<List<FrequentlyQuestionTranslation>>(input.Translations);
            var frequentlyQuestion = ObjectMapper.Map<FrequentlyQuestion>(input);
            frequentlyQuestion.IsActive = true;
            await Repository.InsertAsync(frequentlyQuestion);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return ObjectMapper.Map<FrequentlyQuestionDetailsDto>(frequentlyQuestion);

        }
        public override async Task<FrequentlyQuestionDetailsDto> UpdateAsync(UpdateFrequentlyQuestionDto input)
        {
            var frequentlyQuestion = await _frequentlyQuestionManager.GetEntityByIdAsync(input.Id);
            if (frequentlyQuestion is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound));
            frequentlyQuestion.Translations.Clear();
            MapToEntity(input, frequentlyQuestion);
            frequentlyQuestion.LastModificationTime = DateTime.UtcNow;
            await Repository.UpdateAsync(frequentlyQuestion);
            return MapToEntityDto(frequentlyQuestion);
        }

        public override async Task DeleteAsync(EntityDto<int> input)
        {
            var frequentlyQuestion = await _frequentlyQuestionManager.GetEntityByIdAsync(input.Id);
            if (frequentlyQuestion is null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound));
            await Repository.HardDeleteAsync(frequentlyQuestion);
        }

        protected override IQueryable<FrequentlyQuestion> CreateFilteredQuery(PagedFrequentlyQuestionResultRequestDto input)
        {
        var data = base.CreateFilteredQuery(input);
        data = data.Include(x => x.Translations.Where(t => !t.IsDeleted));
            return data;

        }
        protected override IQueryable<FrequentlyQuestion> ApplySorting(IQueryable<FrequentlyQuestion> query, PagedFrequentlyQuestionResultRequestDto input)
        {
            return query.OrderBy(x => x.Id);
        }


    }
}
