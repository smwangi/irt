namespace Irt.Application.Configuration.Emails
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(EmailMessage message)
        {
            // send email
            return Task.CompletedTask;
        }
    }
}