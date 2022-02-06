using Reminder.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reminder.Core.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> CreateNotification(Notification notification);
        Task DeleteNotification(Notification notification);
        Task<Notification> GetNotification(int id);
        Task<IEnumerable<Notification>> GetNotifications();
        Task<IEnumerable<Notification>> GetCurrentNotifications();
        Task<Notification> UpdateNotification(Notification notification);
        Task DeleteNotifications(IEnumerable<Notification> notifications);
        Task<IEnumerable<Notification>> GetNotificationsByTodoId(int todoId);
    }
}
