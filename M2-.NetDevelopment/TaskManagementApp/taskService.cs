using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.Models;

namespace TaskManagementApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetTasks(
            string? status,
            int? userId)
        {
            var query =
                _context.Tasks.AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(
                    x => x.Status == status);
            }

            if (userId.HasValue)
            {
                query = query.Where(
                    x => x.UserId == userId);
            }

            return await query.ToListAsync();
        }

        public async Task<TaskItem> CreateTask(
            TaskItem task)
        {
            _context.Tasks.Add(task);

            await _context.SaveChangesAsync();

            return task;
        }

        public async Task DeleteTask(int id)
        {
            var task =
                await _context.Tasks.FindAsync(id);

            if (task != null)
            {
                _context.Tasks.Remove(task);

                await _context.SaveChangesAsync();
            }
        }
    }
}