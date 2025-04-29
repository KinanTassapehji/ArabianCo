using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using ArabianCo.CrudAppServiceBase;
using ArabianCo.Domain.Questions;
using ArabianCo.Questions.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ArabianCo.Questions;

public class QuestionAppService : ArabianCoAsyncCrudAppService<Question, QuestionDto, int, QuestionDto, PagedQuestionResultRequest, CreateQuestionDto, UpdateQuestionDto>, IQuestionAppService
{
    public QuestionAppService(IRepository<Question, int> repository) : base(repository)
    {
    }
    public override async Task<QuestionDto> GetAsync(EntityDto<int> input)
    {
        var entity = await Repository.GetAsync(input.Id);
        var entityDto = MapToEntityDto(entity);
        entity.IsRead = true;
        entity.LastModificationTime = DateTime.UtcNow;
        await Repository.UpdateAsync(entity);
        return entityDto;
    }
    protected override IQueryable<Question> CreateFilteredQuery(PagedQuestionResultRequest input)
    {
        var data = base.CreateFilteredQuery(input);
        if (input.IsRead.HasValue)
            data = data.Where(x => x.IsRead == input.IsRead.Value);
        return data;
    }
    protected override IQueryable<Question> ApplySorting(IQueryable<Question> query, PagedQuestionResultRequest input)
    {
        var data = base.ApplySorting(query, input);
        data = data.OrderByDescending(x => x.CreationTime);
        return data;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<QuestionDto> UpdateAsync(UpdateQuestionDto input)
    {
        return base.UpdateAsync(input);
    }
    public override Task DeleteAsync(EntityDto<int> input)
    {
        return base.DeleteAsync(input);
    }
}
