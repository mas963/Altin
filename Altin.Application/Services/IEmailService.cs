using Altin.Application.Common.Email;

namespace Altin.Application.Services;

public interface IEmailService
{
    Task SendEmailAsync(EmailMessage emailMessage);
}