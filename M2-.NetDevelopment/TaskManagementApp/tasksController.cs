using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;
using TaskManagementApi.Services;

namespace TaskManagementApi.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(
            ITaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks(
            string? status,
            int? userId)
        {
            return Ok(
                await _service.GetTasks(
                    status,
                    userId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(
            TaskItem task)
        {
            return Ok(
                await _service.CreateTask(task));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(
            int id)
        {
            await _service.DeleteTask(id);

            return NoContent();
        }
    }
}