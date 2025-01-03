using Irt.Application.Configuration.Notifications;
using MediatR;

namespace Irt.Application.Configuration.Emails
{
    public class EmailHandler : INotificationHandler<DatasourceAddedNotification>
    {
        IEmailSender _emailSender;
        public EmailHandler(IEmailSender emailSender) => _emailSender = emailSender;
        public async Task Handle(DatasourceAddedNotification notification, CancellationToken cancellationToken)
        {
            var emailMessage = new EmailMessage(
                from: "Sam",
                to: "Wan",
                content: notification?.ToString() ?? string.Empty
            );
            await this._emailSender.SendEmailAsync(emailMessage);
        }
    }
}