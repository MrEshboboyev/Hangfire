using System.Net;
using System.Net.Mail;
using HangfireDemo.Shared.Services;

namespace HangfireDemo.Producer;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(string fromEmail, string password)
    {   
        var email = new MailMessage();
        email.From = new MailAddress(fromEmail);
        email.Subject = "Hangfire Job";
        email.To.Add(new MailAddress("examplemail@mail.com"));
        email.Body = "<html><body>Hello, this is an email sent from HangfireJob</body></html>";
        email.IsBodyHtml = true;

        using var smtpClient = new SmtpClient();
        smtpClient.Host = "smtp.gmail.com";
        smtpClient.Port = 587;
        smtpClient.Credentials = new NetworkCredential(fromEmail, password);
        smtpClient.EnableSsl = true;
        
        await smtpClient.SendMailAsync(email);
    }
}