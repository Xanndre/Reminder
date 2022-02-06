using Microsoft.EntityFrameworkCore;
using Reminder.Core.Entities;
using Reminder.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reminder.Infrastructure.DataAccess.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Notification> CreateNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task DeleteNotification(Notification notification)
        {
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<Notification> GetNotification(int id)
        {
            return await _context.Notifications
                .AsNoTracking()
                .FirstAsync(n => n.Id == id);
        }

        public async Task<IEnumerable<Notification>> GetNotifications()
        {
            return await _context.Notifications
                .Include(n => n.Todo)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetCurrentNotifications()
        {
            return await _context.Notifications
                .Include(n => n.Todo)
                .Where(n => !n.IsCompleted && n.Date <= DateTime.Now)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Notification> UpdateNotification(Notification notification)
        {
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task DeleteNotifications(IEnumerable<Notification> notifications)
        {
            _context.Notifications.RemoveRange(notifications);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByTodoId(int todoId)
        {
            return await _context.Notifications
                .Where(n => n.TodoId == todoId)
                .ToListAsync();
        }
    }
}
