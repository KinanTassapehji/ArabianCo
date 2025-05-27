using Abp.Application.Services;
using Abp.Configuration;
using Abp.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ArabianCo.EmailAppService;

public class EmailService : ApplicationService, IEmailService
{
    private readonly IEmailSender _emailSender;
    private readonly ISettingManager _settingManager;

    public EmailService(IEmailSender emailSender, ISettingManager settingManager)
    {
        _emailSender = emailSender;
        _settingManager = settingManager;
    }
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task SendEmailAsync(List<string> emails, string title, string body)
    {
        var username = await _settingManager.GetSettingValueAsync(EmailSettingNames.Smtp.UserName);
        var password = await _settingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Password);
        var smtpClient = new SmtpClient("smtp.office365.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(username, password),
            UseDefaultCredentials = false,
        };

        var mail = new MailMessage
        {
            From = new MailAddress("support@arabianco.com"),
            Subject = title,
            Body = body,
        };
        foreach (var item in emails)
        {
            mail.To.Add(item);
        }


        smtpClient.Send(mail);
    }
}
