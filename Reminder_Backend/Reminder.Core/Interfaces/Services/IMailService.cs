using Reminder.Core.Entities;
using System.Threading.Tasks;

namespace Reminder.Core.Interfaces.Services
{
    public interface IMailService
    {
        Task SendReminder(Notification notification);
        Task SendReminders();
    }
}
