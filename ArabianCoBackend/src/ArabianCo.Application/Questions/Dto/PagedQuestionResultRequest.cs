using Abp.Application.Services.Dto;

namespace ArabianCo.Questions.Dto;

public class PagedQuestionResultRequest:PagedResultRequestDto
{
    public bool? IsRead { get; set; }
}
