using Reminder.Core.Interfaces.Services;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Reminder.Core.Resources;
using Reminder.Core.Interfaces.Repositories;
using Reminder.Core.Config;
using Microsoft.Extensions.Options;
using Reminder.Core.Entities;

namespace Reminder.Core.Services
{
    public class MailService : IMailService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IWeatherService _weatherService;
        private readonly MailOptions _mailOptions;

        public MailService(INotificationRepository notificationRepository,
            IWeatherService weatherService,
            IOptions<MailOptions> mailOptions)
        {
            _notificationRepository = notificationRepository;
            _weatherService = weatherService;
            _mailOptions = mailOptions.Value;
        }

        public async Task SendReminders()
        {
            var notifications = await _notificationRepository.GetCurrentNotifications();

            foreach (var notification in notifications)
            {
                await SendReminder(notification);

                notification.IsCompleted = true;
                await _notificationRepository.UpdateNotification(notification);
            }
        }

        public async Task SendReminder(Notification notification)
        {
            var message = new MimeMessage();
            var bodyBuilder = new BodyBuilder();

            var weather = await _weatherService.GetCurrentWeather();

            bodyBuilder.HtmlBody = string.Format(MailTemplates.ReminderTemplate,
                weather.Temperature,
                weather.Humidity,
                weather.Description,
                notification.Todo.Title,
                notification.Todo.Description,
                notification.Todo.Date.ToShortDateString(),
                notification.Todo.Date.ToShortTimeString());

            message.From.Add(new MailboxAddress(_mailOptions.AppName, _mailOptions.Address));
            message.To.Add(MailboxAddress.Parse(notification.Email));

            message.Subject = notification.Todo.Title;

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(_mailOptions.Host, 465, true);
                await client.AuthenticateAsync(_mailOptions.Address, _mailOptions.Password);

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
