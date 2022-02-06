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
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public TodoService(ITodoRepository todoRepository, IMapper mapper, INotificationService notificationService)
        {
            _todoRepository = todoRepository;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task<TodoDTO> CreateTodo(TodoDTO todo)
        {
            var entity = _mapper.Map<Todo>(todo);
            if (entity.Date < DateTime.Now)
            {
                throw new Exception("Invalid date");
            }
            var dto = await _todoRepository.CreateTodo(entity);
            return _mapper.Map<TodoDTO>(dto);
        }

        public async Task DeleteTodo(int id)
        {
            var dto = await GetTodo(id);
            await _todoRepository.DeleteTodo(_mapper.Map<Todo>(dto));
        }

        public async Task<TodoDTO> GetTodo(int id)
        {
            return _mapper.Map<TodoDTO>(await _todoRepository.GetTodo(id));
        }

        public async Task<IEnumerable<TodoDTO>> GetTodos()
        {
            return _mapper.Map<IEnumerable<TodoDTO>>(await _todoRepository.GetTodos());
        }

        public async Task<TodoDTO> UpdateTodo(TodoDTO todo)
        {
            var entity = _mapper.Map<Todo>(todo);
            if (entity.Date < DateTime.Now)
            {
                throw new Exception("Invalid date");
            }

            var dto = await _todoRepository.UpdateTodo(entity);
            await _notificationService.DeleteNotifications(entity);

            return _mapper.Map<TodoDTO>(dto);
        }
    }
}
