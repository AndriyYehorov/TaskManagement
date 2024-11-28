using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Services;
using TaskManagement.Infrastructure.Authentication;

namespace TaskManagement.API.Controllers
{
    [Route("tasks")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;        

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }        

        [HttpGet]        
        public async Task<IActionResult> GetAllTasksAsync()
        {            
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTaskAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskAsync([FromBody] TaskDTO taskDTO)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var response = await _taskService.CreateTaskAsync(taskDTO, userId);

            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTaskAsync(Guid id, TaskDTO taskDTO)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTaskAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
