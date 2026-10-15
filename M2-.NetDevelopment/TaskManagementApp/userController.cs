using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;
using TaskManagementApi.Services;

namespace TaskManagementApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(
            IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(
                await _service.GetUsers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(
            int id)
        {
            var user =
                await _service.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(
            User user)
        {
            var createdUser =
                await _service.CreateUser(user);

            return CreatedAtAction(
                nameof(GetUser),
                new { id = createdUser.Id },
                createdUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(
            int id)
        {
            await _service.DeleteUser(id);

            return NoContent();
        }
    }
}