using Microsoft.EntityFrameworkCore;
using Reminder.Core.Entities;
using Reminder.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reminder.Infrastructure.DataAccess.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext _context;

        public TodoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Todo> CreateTodo(Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return todo;            
        }

        public async Task DeleteTodo(Todo todo)
        {
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
        }

        public async Task<Todo> GetTodo(int id)
        {
            return await _context.Todos
                .Include(t => t.Notifications)
                .AsNoTracking()
                .FirstAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Todo>> GetTodos()
        {
            return await _context.Todos
                .Include(t => t.Notifications)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Todo> UpdateTodo(Todo todo)
        {
            _context.Todos.Update(todo);
            await _context.SaveChangesAsync();
            return todo;
        }
    }
}
