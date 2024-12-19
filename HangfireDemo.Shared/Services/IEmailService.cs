namespace HangfireDemo.Shared.Services;

public interface IEmailService
{
    Task SendEmailAsync(string email, string password);
}