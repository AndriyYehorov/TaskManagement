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
        public async Task<IActionResult> GetAllTasksAsync(
            [FromQuery] TaskFilter? filter,
            string? sortColumn, 
            string? sortOrder, 
            int page = 1, 
            int pageSize = 5)
        {         
            var response = await _taskService.ReadAllTasksAsync(
                filter,
                sortColumn, 
                sortOrder, 
                page, 
                pageSize, 
                GetUserId());

            if (!response.IsSuccess)
            {                
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTaskAsync(string id)
        {
            var response = await _taskService.ReadTaskAsync(id, GetUserId());

            if (!response.IsSuccess)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskAsync([FromBody] TaskDTO taskDTO)
        {       
            var response = await _taskService.CreateTaskAsync(taskDTO, GetUserId());

            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTaskAsync(string id, [FromBody] TaskDTO taskDTO)
        {
            var response = await _taskService.UpdateTaskAsync(id, taskDTO, GetUserId());

            if (!response.IsSuccess) 
            { 
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTaskAsync(string id)
        {
            var response = await _taskService.DeleteTaskAsync(id, GetUserId());

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        private string GetUserId()
        {
            return HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
