using Reminder.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reminder.Core.Interfaces.Repositories
{
    public interface ITodoRepository
    {
        Task<Todo> CreateTodo(Todo todo);
        Task DeleteTodo(Todo todo);
        Task<Todo> GetTodo(int id);
        Task<IEnumerable<Todo>> GetTodos();
        Task<Todo> UpdateTodo(Todo todo);
    }
}
