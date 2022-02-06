using Reminder.Core.DTOs;
using Reminder.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reminder.Core.Interfaces.Services
{
    public interface INotificationService
    {
        Task<NotificationDTO> CreateNotification(NotificationDTO notification);
        Task DeleteNotification(int id);
        Task<NotificationDTO> GetNotification(int id);
        Task<IEnumerable<NotificationDTO>> GetNotifications();
        Task<NotificationDTO> UpdateNotification(NotificationDTO notification);
        Task DeleteNotifications(Todo todo);
    }
}
