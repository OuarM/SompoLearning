using TaskManagementApi.Models;

namespace TaskManagementApi.Services
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetTasks(
            string? status,
            int? userId);

        Task<TaskItem> CreateTask(
            TaskItem task);

        Task DeleteTask(int id);
    }
}