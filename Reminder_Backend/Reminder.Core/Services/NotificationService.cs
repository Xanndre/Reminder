using AutoMapper;
using Reminder.Core.DTOs;
using Reminder.Core.Entities;
using Reminder.Core.Interfaces.Repositories;
using Reminder.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reminder.Core.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public NotificationService(
            INotificationRepository notificationRepository,
            ITodoRepository todoRepository,
            IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _todoRepository = todoRepository;
            _mapper = mapper;
        }
        public async Task<NotificationDTO> CreateNotification(NotificationDTO notification)
        {
            var todo = await _todoRepository.GetTodo(notification.TodoId);
            var entity = _mapper.Map<Notification>(notification);

            if (entity.Date < DateTime.Now || entity.Date > todo.Date)
            {
                throw new Exception("Invalid date");
            }

            var dto = await _notificationRepository.CreateNotification(entity);
            return _mapper.Map<NotificationDTO>(dto);
        }

        public async Task DeleteNotification(int id)
        {
            var dto = await GetNotification(id);
            await _notificationRepository.DeleteNotification(_mapper.Map<Notification>(dto));
        }

        public async Task<NotificationDTO> GetNotification(int id)
        {
            return _mapper.Map<NotificationDTO>(await _notificationRepository.GetNotification(id));
        }

        public async Task<IEnumerable<NotificationDTO>> GetNotifications()
        {
            return _mapper.Map<IEnumerable<NotificationDTO>>(await _notificationRepository.GetNotifications());
        }

        public async Task<NotificationDTO> UpdateNotification(NotificationDTO notification)
        {
            var todo = await _todoRepository.GetTodo(notification.TodoId);
            var entity = _mapper.Map<Notification>(notification);

            if (entity.Date < DateTime.Now || entity.Date > todo.Date)
            {
                throw new Exception("Invalid date");
            }

            var dto = await _notificationRepository.UpdateNotification(entity);
            return _mapper.Map<NotificationDTO>(dto);
        }

        public async Task DeleteNotifications(Todo todo)
        {
            var notificationsToDelete = await _notificationRepository.GetNotificationsByTodoId(todo.Id);
            await _notificationRepository.DeleteNotifications(notificationsToDelete);
        }
    }
}
