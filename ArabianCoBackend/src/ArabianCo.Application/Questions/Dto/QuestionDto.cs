using Abp.Application.Services.Dto;

namespace ArabianCo.Questions.Dto;

public class QuestionDto:EntityDto
{
    public string Email { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string YourQuestion { get; set; }
    public bool IsRead { get; set; }
}
