using Abp.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArabianCo.EmailAppService;

public interface IEmailService : IApplicationService
{
    Task SendEmailAsync(List<string> emails, string title, string body);
}
