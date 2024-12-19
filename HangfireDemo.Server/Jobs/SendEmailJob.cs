using HangfireDemo.Server.Options;
using HangfireDemo.Shared.Jobs;
using HangfireDemo.Shared.Services;
using Microsoft.Extensions.Options;

namespace HangfireDemo.Server.Jobs;

public class SendEmailJob(
    IEmailService emailService,
    IOptions<ServerOptions> serverOptions) : ISendEmailJob
{
    private readonly IEmailService _emailService = emailService;
    
    private readonly string _email = serverOptions.Value.Email;
    private readonly string _password = serverOptions.Value.Password;
    
    public async Task ExecuteAsync()
    {
        await _emailService.SendEmailAsync(_email, _password);
    }
}