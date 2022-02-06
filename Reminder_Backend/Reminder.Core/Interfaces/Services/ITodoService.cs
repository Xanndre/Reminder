using Reminder.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reminder.Core.Interfaces.Services
{
    public interface ITodoService
    {
        Task<TodoDTO> CreateTodo(TodoDTO todo);
        Task DeleteTodo(int id);
        Task<TodoDTO> GetTodo(int id);
        Task<IEnumerable<TodoDTO>> GetTodos();
        Task<TodoDTO> UpdateTodo(TodoDTO todo);
    }
}
